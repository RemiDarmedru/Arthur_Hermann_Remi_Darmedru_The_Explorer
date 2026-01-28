using UnityEngine;
using AK.Wwise;
using System.Collections;

public class InfoZoneAudio : MonoBehaviour
{
    [Header("Wwise Events")] public AK.Wwise.Event Play_Info_Zone_In;
    public AK.Wwise.Event Play_Info_Zone_Out;

    [Header("Audio Source")] public GameObject AudioSource;

    [Header("Settings")] [Tooltip("Délai avant de jouer le son (en secondes)")]
    public float delay = 0.5f;

    void Start()
    {
        if (AudioSource == null)
            AudioSource = gameObject;
    }

    // Fonction appelée quand le joueur entre dans la zone
    public void PlayZoneIn()
    {
        StartCoroutine(PlayZoneInDelayed());
    }

    // Fonction appelée quand le joueur sort de la zone
    public void PlayZoneOut()
    {
        if (Play_Info_Zone_Out != null && AudioSource != null)
        {
            Play_Info_Zone_Out.Post(AudioSource);
            Debug.Log("[InfoZoneAudio] Zone Out");
        }
    }

    private IEnumerator PlayZoneInDelayed()
    {
        yield return new WaitForSeconds(delay);

        if (Play_Info_Zone_In != null && AudioSource != null)
        {
            Play_Info_Zone_In.Post(AudioSource);
            Debug.Log("[InfoZoneAudio] Zone In (après délai)");
        }
    }
}
    
    