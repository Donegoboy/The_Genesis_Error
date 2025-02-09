using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public bool isLocked = true;
    private GameManager gameManager;
    public Sprite unlockSprite;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Unlock()
    {
        isLocked = false;
        spriteRenderer.sprite = unlockSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isLocked)
        {          
            if (gameManager != null)
            {
                gameManager.LevelCompleted();
            }
        }
    }
}
