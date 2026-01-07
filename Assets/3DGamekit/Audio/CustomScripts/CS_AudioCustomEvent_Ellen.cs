using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AudioCustomEvent_Ellen : MonoBehaviour
{
    public AK.Wwise.Event MC_FT;
    public GameObject AudioSource;
    public GameObject Ellen;
    
    public void MC_FT_Play(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AkSoundEngine.SetSwitch("MC_FOOTSTEPS_STATE", animationEvent.stringParameter, Ellen);
            Debug.Log(animationEvent.stringParameter);
            MC_FT.Post(AudioSource);
        }
    }
}
