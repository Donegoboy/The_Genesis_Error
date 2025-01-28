using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{   
    private GameManager gameManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager = FindAnyObjectByType<GameManager>();
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            gameManager.onDeath();
        }
    }
}