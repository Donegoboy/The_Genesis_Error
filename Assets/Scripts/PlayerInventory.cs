using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int numberOfKeys = 0;
    public int keysNeededToUnlockExit = 3; // Set this in the Inspector
    public Image[] keyImages; // Array to hold the UI Images for the keys

    public Sprite blackKeySprite;
    public Sprite coloredKeySprite;

    private void Start()
    {
        // Initialize all key images to the black key sprite
        foreach (Image keyImage in keyImages)
        {
            if (keyImage != null)
            {
                keyImage.sprite = blackKeySprite;
            }
        }
    }

    public void AddKey()
    {
        if (numberOfKeys < keyImages.Length)
        {
            // Update the corresponding UI key image to the colored sprite
            keyImages[numberOfKeys].sprite = coloredKeySprite;

            numberOfKeys++;
            Debug.Log("Key collected! Total keys: " + numberOfKeys);

            // Check if enough keys are collected to unlock the exit
            if (numberOfKeys >= keysNeededToUnlockExit)
            {
                UnlockExit();
            }
        }
    }



    private void UnlockExit()
    {
        // Find the Exit object and unlock it
        GameObject exit = GameObject.FindGameObjectWithTag("Exit");
        if (exit != null)
        {
            Exit exitScript = exit.GetComponent<Exit>();
            if (exitScript != null)
            {
                exitScript.Unlock();
            }
        }
        else //Error handling in case Exit is not defined:
        {
            Debug.LogError("UnlockExit failed, make sure Exit tag is defined");
        }
    }
}
