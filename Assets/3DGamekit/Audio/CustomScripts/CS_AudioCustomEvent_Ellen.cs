using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AudioCustomEvent_Ellen : MonoBehaviour
{
    public AK.Wwise.Event MC_FT;
    public AK.Wwise.Event MC_JUMP;
    public AK.Wwise.Event MC_LAND;
    public AK.Wwise.Event MC_ROLL;
    public AK.Wwise.Event MC_COMBO_1;
    public AK.Wwise.Event MC_COMBO_2;
    public AK.Wwise.Event MC_COMBO_3;
    public AK.Wwise.Event MC_COMBO_4;
    
    public GameObject AudioSource;
    
    private string currentMaterial;
    
    public void MC_FT_Play(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            DetectGroundMaterials();
            AkSoundEngine.SetSwitch("MC_FOOTSTEPS_STATE", animationEvent.stringParameter, AudioSource);
            Debug.Log(animationEvent.stringParameter);
            MC_FT.Post(AudioSource);
        }
    }

    public void MC_JUMP_Play()
    {
        DetectGroundMaterials();
        MC_JUMP.Post(AudioSource);
    }

    public void MC_LAND_Play()
    {
        DetectGroundMaterials();
        MC_LAND.Post(AudioSource);
    }

    public void DetectGroundMaterials()
    {
        RaycastHit hit;
        Vector3 ray = transform.TransformDirection(Vector3.down);
        
        if (Physics.Raycast(transform.position, ray, out hit, 10f))
        {
            if (hit.collider != null)
            {
                string newMaterial = "Mud";

                newMaterial = hit.collider.tag;
                
                if (newMaterial != currentMaterial)
                {
                    currentMaterial = newMaterial;
                    AkSoundEngine.SetSwitch("MC_FOOTSTEPS_MATERIAL", currentMaterial, AudioSource);
                    Debug.Log(currentMaterial);
                }
            }
        }
    }

    public void MC_ROLL_Play()
    {
        DetectGroundMaterials();
        MC_ROLL.Post(AudioSource);
    }
    
    public void MC_COMBO_1_Play()
    {
        MC_COMBO_1.Post(AudioSource);
    }

    public void MC_COMBO_2_Play()
    {
        MC_COMBO_2.Post(AudioSource);
    }

    public void MC_COMBO_3_Play()
    {
        MC_COMBO_3.Post(AudioSource);
    }

    public void MC_COMBO_4_Play()
    {
        MC_COMBO_4.Post(AudioSource);
    }
    
}
