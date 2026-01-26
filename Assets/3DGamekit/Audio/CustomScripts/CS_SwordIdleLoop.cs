using UnityEngine;
using AK.Wwise;

public class SwordIdleLoop : MonoBehaviour
{
    [Header("Wwise Events")]
    public AK.Wwise.Event Play_SwordIdle;
    public AK.Wwise.Event Stop_SwordIdle;
    public AK.Wwise.Event Play_SwordTake;
    
    [Header("Audio Source")]
    public GameObject AudioSource;
    
    private bool isPlaying = false;
    
    void Start()
    {
        if (AudioSource == null)
            AudioSource = gameObject;
        
        StartIdleLoop();
    }
    
    public void StartIdleLoop()
    {
        if (!isPlaying && AudioSource != null && Play_SwordIdle != null)
        {
            Play_SwordIdle.Post(AudioSource);
            isPlaying = true;
            Debug.Log($"[SwordIdle] Loop démarrée sur {gameObject.name}");
        }
    }
    
    public void StopIdleLoop()
    {
        Debug.Log($"[SwordIdle] StopIdleLoop appelé sur {gameObject.name}");
        
        if (AudioSource != null)
        {
            if (Stop_SwordIdle != null)
            {
                Stop_SwordIdle.Post(AudioSource);
                Debug.Log("[SwordIdle] Stop event posté");
            }
            else
            {
                AkSoundEngine.StopAll(AudioSource);
                Debug.Log("[SwordIdle] StopAll appelé");
            }
            isPlaying = false;
            if (Play_SwordTake != null && AudioSource != null)
            {
                Play_SwordTake.Post(AudioSource);
                Debug.Log("[SwordIdle] Son de pickup joué");
            }
        }
    }
    
    void OnDisable()
    {
        StopIdleLoop();
    }
}