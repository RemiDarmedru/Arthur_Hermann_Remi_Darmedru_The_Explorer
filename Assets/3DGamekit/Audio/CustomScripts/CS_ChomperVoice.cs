using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class ChomperVoice : MonoBehaviour
{
    [Header("Wwise Events")]
    public AK.Wwise.Event Play_ChomperVoiceIdle;
    public AK.Wwise.Event Play_ChomperVoiceGrunt;
    public AK.Wwise.Event Play_ChomperVoiceDeath;
    
    [Header("Audio Source")]
    public GameObject AudioSource;
    
    [Header("Idle Settings")]
    [Tooltip("Temps minimum entre deux idle (en secondes)")]
    public float idleMinDelay = 3f;
    [Tooltip("Temps maximum entre deux idle (en secondes)")]
    public float idleMaxDelay = 8f;
    
    private float nextIdleTime;
    private bool isDead = false;
    
    void Start()
    {
        // Si AudioSource n'est pas assigné, utilise ce GameObject
        if (AudioSource == null)
        {
            AudioSource = gameObject;
        }
        
        // Planifie le premier idle
        ScheduleNextIdle();
    }
    
    void Update()
    {
        // Joue les idle automatiquement
        if (!isDead && Time.time >= nextIdleTime)
        {
            PlayIdle();
            ScheduleNextIdle();
        }
    }
    
    // Planifie le prochain idle aléatoire
    private void ScheduleNextIdle()
    {
        nextIdleTime = Time.time + Random.Range(idleMinDelay, idleMaxDelay);
    }
    
    // Fonction appelée pour les idle (peut être appelée par Animation Event aussi)
    public void PlayIdle()
    {
        if (!isDead && AudioSource != null)
        {
            Play_ChomperVoiceIdle.Post(AudioSource);
            Debug.Log($"[ChomperVoice] Idle joué sur {gameObject.name}");
        }
    }
    
    // Fonction appelée pour les grognements (Animation Event d'attaque par exemple)
    public void PlayGrunt()
    {
        if (!isDead && AudioSource != null)
        {
            Play_ChomperVoiceGrunt.Post(AudioSource);
            Debug.Log($"[ChomperVoice] Grunt joué sur {gameObject.name}");
        }
    }
    
    // Fonction appelée pour la mort (Animation Event de mort)
    public void PlayDeath()
    {
        if (AudioSource != null)
        {
            Play_ChomperVoiceDeath.Post(AudioSource);
            isDead = true; // Empêche les autres sons de se jouer
            Debug.Log($"[ChomperVoice] Death joué sur {gameObject.name}");
        }
    }
    
    // Fonction utilitaire pour stopper tous les sons (optionnel)
    public void StopAllVoices()
    {
        Play_ChomperVoiceIdle.Stop(AudioSource);
        Play_ChomperVoiceGrunt.Stop(AudioSource);
        Play_ChomperVoiceDeath.Stop(AudioSource);
    }
    
    // Si le Chomper est détruit, arrête les sons
    void OnDestroy()
    {
        StopAllVoices();
    }
}