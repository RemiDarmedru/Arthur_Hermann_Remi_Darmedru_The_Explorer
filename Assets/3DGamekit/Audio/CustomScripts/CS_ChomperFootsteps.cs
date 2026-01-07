using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class Footsteps : MonoBehaviour
{
    public AK.Wwise.Event Play_ChomperFootsteps;
    public GameObject AudioSource;
    
    public void PlayFootsteps()
    {
        Debug.Log("PlayFootsteps appelé !");
        
        if (AudioSource == null)
        {
            Debug.LogError("AudioSource est NULL !");
            return;
        }
        
        Debug.Log("Post sur : " + AudioSource.name);
        Play_ChomperFootsteps.Post(AudioSource);
    }
}