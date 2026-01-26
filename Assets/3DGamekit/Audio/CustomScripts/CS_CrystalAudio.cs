using UnityEngine;
using AK.Wwise;

public class CS_CrystalAudio : MonoBehaviour
{
    [Header("Wwise Events")]
    public AK.Wwise.Event Play_CrystalActivate;
    
    [Header("Audio Source")]
    public GameObject AudioSource;
    
    void Start()
    {
        if (AudioSource == null)
            AudioSource = gameObject;
    }
    
    public void PlayActivateSound()
    {
        if (Play_CrystalActivate != null && AudioSource != null)
        {
            Play_CrystalActivate.Post(AudioSource);
            Debug.Log($"[CrystalAudio] Son d'activation joué sur {gameObject.name}");
        }
    }
}