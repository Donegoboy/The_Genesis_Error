using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Key : MonoBehaviour
{
    public GameObject keyCollectVFXPrefab;
    public AudioClip keyCollectSFX;

    public void Collect()
    {
        if (keyCollectSFX != null)
        {
            GameObject soundObject = new GameObject("KeySFX");
            soundObject.transform.position = transform.position;

            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.clip = keyCollectSFX;
            audioSource.spatialBlend = 0f;
            audioSource.pitch = 2.5f;
            audioSource.Play();
            Destroy(soundObject, audioSource.clip.length);
        }


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerInventory inventory = player.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddKey();
            }
        }

        if (keyCollectVFXPrefab != null)
        {
            Instantiate(keyCollectVFXPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}