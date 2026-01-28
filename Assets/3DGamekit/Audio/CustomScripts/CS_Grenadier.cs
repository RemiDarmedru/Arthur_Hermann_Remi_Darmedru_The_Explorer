using UnityEngine;
using AK.Wwise;

public class CS_GrenadierAudio : MonoBehaviour
{
    [Header("Wwise Events - Attacks")]
    public AK.Wwise.Event Play_Grenadier_CloseRangeAttack;
    public AK.Wwise.Event Play_Grenadier_MeleeAttack;
    public AK.Wwise.Event Play_Grenadier_RangeAttack;
    public AK.Wwise.Event Play_Grenadier_RangeAttack2;
    
    [Header("Wwise Events - Movement")]
    public AK.Wwise.Event Play_Grenadier_Walk;
    public AK.Wwise.Event Play_Grenadier_WalkFast;
    public AK.Wwise.Event Play_Grenadier_Idle;
    
    [Header("Wwise Events - Reactions")]
    public AK.Wwise.Event Play_Grenadier_Hit;
    public AK.Wwise.Event Play_Grenadier_Death;
    
    [Header("Audio Source")]
    public GameObject AudioSource;
    
    void Start()
    {
        if (AudioSource == null)
            AudioSource = gameObject;
    }
    
    // ========== ATTACKS ==========
    
    public void PlayCloseRangeAttack()
    {
        if (Play_Grenadier_CloseRangeAttack != null && AudioSource != null)
        {
            Play_Grenadier_CloseRangeAttack.Post(AudioSource);
            Debug.Log("[GrenadierAudio] CloseRangeAttack");
        }
    }
    
    public void PlayMeleeAttack()
    {
        if (Play_Grenadier_MeleeAttack != null && AudioSource != null)
        {
            Play_Grenadier_MeleeAttack.Post(AudioSource);
            Debug.Log("[GrenadierAudio] MeleeAttack");
        }
    }
    
    public void PlayRangeAttack()
    {
        if (Play_Grenadier_RangeAttack != null && AudioSource != null)
        {
            Play_Grenadier_RangeAttack.Post(AudioSource);
            Debug.Log("[GrenadierAudio] RangeAttack");
        }
    }
    
    public void PlayRangeAttack2()
    {
        if (Play_Grenadier_RangeAttack2 != null && AudioSource != null)
        {
            Play_Grenadier_RangeAttack2.Post(AudioSource);
            Debug.Log("[GrenadierAudio] RangeAttack2");
        }
    }
    
    // ========== MOVEMENT ==========
    
    public void PlayWalk()
    {
        if (Play_Grenadier_Walk != null && AudioSource != null)
        {
            Play_Grenadier_Walk.Post(AudioSource);
            Debug.Log("[GrenadierAudio] Walk");
        }
    }
    
    public void PlayWalkFast()
    {
        if (Play_Grenadier_WalkFast != null && AudioSource != null)
        {
            Play_Grenadier_WalkFast.Post(AudioSource);
            Debug.Log("[GrenadierAudio] WalkFast");
        }
    }
    
    public void PlayIdle()
    {
        if (Play_Grenadier_Idle != null && AudioSource != null)
        {
            Play_Grenadier_Idle.Post(AudioSource);
            Debug.Log("[GrenadierAudio] Idle");
        }
    }
    
    // ========== REACTIONS ==========
    
    public void PlayHit()
    {
        if (Play_Grenadier_Hit != null && AudioSource != null)
        {
            Play_Grenadier_Hit.Post(AudioSource);
            Debug.Log("[GrenadierAudio] Hit");
        }
    }
    
    public void PlayDeath()
    {
        if (Play_Grenadier_Death != null && AudioSource != null)
        {
            // Stop tous les autres sons avant la mort
            AkSoundEngine.StopAll(AudioSource);
            
            Play_Grenadier_Death.Post(AudioSource);
            Debug.Log("[GrenadierAudio] Death");
        }
    }
}