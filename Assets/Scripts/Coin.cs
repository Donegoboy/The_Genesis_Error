using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 200;
    public GameObject coinCollectVFXPrefab;
    public AudioClip coinCollectSFX;

    public void Collect()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(coinValue);
            }

            if (coinCollectVFXPrefab != null)
            {
                GameObject vfxInstance = Instantiate(coinCollectVFXPrefab, transform.position, Quaternion.identity);
            }

            if (coinCollectSFX != null)
            {
                GameObject soundObject = new GameObject("CoinSFX");
                soundObject.transform.position = transform.position;

                AudioSource audioSource = soundObject.AddComponent<AudioSource>();
                audioSource.clip = coinCollectSFX;
                audioSource.spatialBlend = 0f; 
                audioSource.Play();
                Destroy(soundObject, audioSource.clip.length);
            }
            Destroy(gameObject);
        }
    }
}