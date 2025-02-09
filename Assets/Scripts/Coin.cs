using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 200;
    public GameObject coinCollectVFXPrefab;

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
                Instantiate(coinCollectVFXPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

    }
}