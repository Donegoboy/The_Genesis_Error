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
            Debug.Log("Player landed on spikes");
            Destroy(collision.gameObject); // Destroy the player object
            gameManager.onDeath();
        }
    }
}