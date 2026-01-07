using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AudioCustomEvent_Ellen : MonoBehaviour
{
    public AK.Wwise.Event MC_FT;
    public GameObject AudioSource;
    
    public void MC_FT_Play(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AkSoundEngine.SetSwitch("MC_FOOTSTEPS_STATE", animationEvent.stringParameter, AudioSource);
            MC_FT.Post(AudioSource);
        }
    }
}
