using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    private AudioSource audioSource;
    //AudioMenager je bolji nacin - profesionalno
    // Start is called before the first frame update
    //Najbolja stvar je da ako nema audio objekta, koji spawna i despawna audio. Znaci, ne destroy nego pulla i despawn-a. ASSET BUNDLE JE JAKO VAZAN. ISTRAZI!!!!!!!
    //Skripte koje su uvijek jeben za napraviti: Loader scene, Save Scene, Audio menager, Game Menager

    //V= ATTENUATION * FADE * VIRTUAL * PRIORITY -> za zvuk.

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying) 
        {
            Destroy(gameObject);
        }
    }


    //!!!TO DO!!!
}