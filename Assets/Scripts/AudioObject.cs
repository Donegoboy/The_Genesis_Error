using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AudioMenager je bolji nacin - profesionalno
// Start is called before the first frame update
//Najbolja stvar je da ako nema audio objekta, koji spawna i despawna audio. Znaci, ne destroy nego pulla i despawn-a. ASSET BUNDLE JE JAKO VAZAN. ISTRAZI!!!!!!!
//Skripte koje su uvijek ðeben za napraviti: Loader scene, Save Scene, Audio menager, Game Menager

//V= ATTENUATION * FADE * VIRTUAL * PRIORITY -> za zvuk.

public class Audio : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing on " + gameObject.name);
            Destroy(this); // Destroy this script if no AudioSource.  Prevent errors.
            return;
        }
    }

    private void Update()
    {
        // Destroy this GameObject *after* the sound has finished playing.
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}