using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int numberOfKeys = 0; // Current number of keys collected
    public int keysNeededToUnlockExit = 3; // Set this in the Inspector

    public void AddKey()
    {
        numberOfKeys++;
        Debug.Log("Key collected! Total keys: " + numberOfKeys);

        // Check if enough keys are collected to unlock the exit
        if (numberOfKeys >= keysNeededToUnlockExit)
        {
            UnlockExit();
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
