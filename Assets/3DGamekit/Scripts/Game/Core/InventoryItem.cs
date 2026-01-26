using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

namespace Gamekit3D
{
    [RequireComponent(typeof(Collider))]
    public class InventoryItem : MonoBehaviour, IDataPersister
    {
        public string inventoryKey = "";
        public LayerMask layers;
        public bool disableOnEnter = false;

        [HideInInspector]
        new public Collider collider;

        public AudioClip clip;
        public DataSettings dataSettings;
        
        [Header("Wwise Idle Loop")]
        public AK.Wwise.Event Play_ItemIdle;
        public AK.Wwise.Event Stop_ItemIdle;
        public GameObject AudioSource;
        private bool isLoopPlaying = false;

        void OnEnable()
        {
            collider = GetComponent<Collider>();
            PersistentDataManager.RegisterPersister(this);
            if (AudioSource == null)
                AudioSource = gameObject;
                
            if (Play_ItemIdle != null && !isLoopPlaying)
            {
                Play_ItemIdle.Post(AudioSource);
                isLoopPlaying = true;
            }
        }

        void OnDisable()
        {
            PersistentDataManager.UnregisterPersister(this);
            StopIdleLoop();
        }

        void Reset()
        {
            layers = LayerMask.NameToLayer("Everything");
            collider = GetComponent<Collider>();
            collider.isTrigger = true;
            dataSettings = new DataSettings();
        }

        void OnTriggerEnter(Collider other)
        {
            if (layers.Contains(other.gameObject))
            {
                var ic = other.GetComponent<InventoryController>();
                ic.AddItem(inventoryKey);
                StopIdleLoop();
                if (disableOnEnter)
                {
                    gameObject.SetActive(false);
                    Save();
                }
                

            }
        }
        
        private void StopIdleLoop()
        {
            if (isLoopPlaying && AudioSource != null)
            {
                if (Stop_ItemIdle != null)
                {
                    Stop_ItemIdle.Post(AudioSource);
                }
                else
                {
                    AkSoundEngine.StopAll(AudioSource);
                }
                isLoopPlaying = false;
            }
        }

        public void Save()
        {
            PersistentDataManager.SetDirty(this);
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "InventoryItem", false);
        }

        public DataSettings GetDataSettings()
        {
            return dataSettings;
        }

        public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
        {
            dataSettings.dataTag = dataTag;
            dataSettings.persistenceType = persistenceType;
        }

        public Data SaveData()
        {
            return new Data<bool>(gameObject.activeSelf);
        }

        public void LoadData(Data data)
        {
            Data<bool> inventoryItemData = (Data<bool>)data;
            gameObject.SetActive(inventoryItemData.value);
        }
    }
}
