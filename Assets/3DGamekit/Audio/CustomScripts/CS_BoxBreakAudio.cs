using UnityEngine;
using AK.Wwise;

public class BoxBreakAudio : MonoBehaviour
{
    [Header("Wwise Event")]
    public AK.Wwise.Event Play_BoxBreak;
    
    [Header("Audio Source")]
    public GameObject AudioSource;
    
    void Start()
    {
        if (AudioSource == null)
            AudioSource = gameObject;
    }
    
    // Fonction appelée quand la box se casse
    public void PlayBreakSound()
    {
        if (Play_BoxBreak != null && AudioSource != null)
        {
            Play_BoxBreak.Post(AudioSource);
            Debug.Log($"[BoxBreak] Son joué sur {gameObject.name}");
        }
    }
    
    // Alternative : se déclenche automatiquement à la destruction
    void OnDestroy()
    {
        // Joue le son avant que l'objet soit détruit
        if (Play_BoxBreak != null && AudioSource != null)
        {
            // Important : on détache l'AudioSource pour qu'il survive à la destruction
            AkSoundEngine.PostEvent(Play_BoxBreak.Id, AudioSource);
        }
    }
}