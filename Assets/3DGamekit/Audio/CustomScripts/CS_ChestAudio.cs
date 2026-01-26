using UnityEngine;
using AK.Wwise;

public class ChestAudio : MonoBehaviour
{
    [Header("Wwise Events")]
    public AK.Wwise.Event Play_ChestOpen;
    
    [Header("Audio Source")]
    public GameObject AudioSource;
    
    void Start()
    {
        if (AudioSource == null)
            AudioSource = gameObject;
    }
    
    public void PlayOpenSound()
    {
        if (Play_ChestOpen != null && AudioSource != null)
        {
            Play_ChestOpen.Post(AudioSource);
            Debug.Log($"[ChestAudio] Son d'ouverture joué sur {gameObject.name}");
        }
    }
}
