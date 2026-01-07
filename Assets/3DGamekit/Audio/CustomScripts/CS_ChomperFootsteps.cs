using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class Footsteps : MonoBehaviour
{
    public AK.Wwise.Event Play_ChomperFootsteps;
    public GameObject AudioSource;
    public GameObject Chomper;
    
    public void PlayFootsteps(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AkSoundEngine.SetSwitch("CHOMPER_FOOTSTEPS_STATE", animationEvent.stringParameter, Chomper);
        }
        Play_ChomperFootsteps.Post(AudioSource);
    }
}