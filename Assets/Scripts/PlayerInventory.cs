using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int numberOfKeys = 0;
    public int keysNeededToUnlockExit = 3;
    public Image[] keyImages;

    public Sprite blackKeySprite;
    public Sprite coloredKeySprite;

    private void Start()
    {
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
            keyImages[numberOfKeys].sprite = coloredKeySprite;

            numberOfKeys++;

            if (numberOfKeys >= keysNeededToUnlockExit)
            {
                UnlockExit();
            }
        }
    }
    private void UnlockExit()
    {
        GameObject exit = GameObject.FindGameObjectWithTag("Exit");
        if (exit != null)
        {
            Exit exitScript = exit.GetComponent<Exit>();
            if (exitScript != null)
            {
                exitScript.Unlock();
            }
        }
    }
}
