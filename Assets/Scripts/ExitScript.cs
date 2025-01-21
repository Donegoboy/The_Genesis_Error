using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public bool isLocked = true;
    private GameManager gameManager;

    private void Start()
    {
       gameManager = FindObjectOfType<GameManager>();
        
    }

    public void Unlock()
    {
        isLocked = false;
        Debug.Log("Exit unlocked!");

        // Add any visual changes for unlocking (e.g., change sprite, play animation)
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isLocked)
        {
            // Handle level completion
            Debug.Log("Level complete!");

            if (gameManager != null)
            {
                gameManager.LevelCompleted(); // Open the level completion panel
            }
        }
    }
}
