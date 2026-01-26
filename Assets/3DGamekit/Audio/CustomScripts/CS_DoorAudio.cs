using UnityEngine;
using AK.Wwise;

public class CS_DoorAudio : MonoBehaviour
{
    [Header("Wwise Event")]
    public AK.Wwise.Event Play_DoorOpen;
    
    [Header("Audio Source")]
    public GameObject AudioSource;[Header("Settings")]
    
    [Tooltip("Jouer automatiquement quand la porte s'active")]
    public bool playOnEnable = true;
    [Tooltip("Délai avant de jouer le son (en secondes)")]
    public float delay = 0f;
    
    void Start()
    {
        if (AudioSource == null)
            AudioSource = gameObject;
    }
    
    void OnEnable()
    {
        if (playOnEnable)
        {
            if (delay > 0)
                Invoke(nameof(PlayOpenSound), delay);
            else
                PlayOpenSound();
        }
    }

    
    // Fonction à appeler pour jouer le son
    public void PlayOpenSound()
    {
        if (Play_DoorOpen != null && AudioSource != null)
        {
            Play_DoorOpen.Post(AudioSource);
            Debug.Log($"[DoorAudio] Son joué sur {gameObject.name}");
        }
    }
}