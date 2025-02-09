using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Key : MonoBehaviour
{
    public GameObject keyCollectVFXPrefab; // Drag your "Hit_02" prefab here in the Inspector

    public void Collect()
    {
        Debug.Log("Key's Collect() method called!");

        // Find the player and add a key to the inventory
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerInventory inventory = player.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddKey();
                Debug.Log("Key added to inventory");
            }
        }

        // Instantiate the VFX prefab at the key's position
        if (keyCollectVFXPrefab != null)
        {
            Instantiate(keyCollectVFXPrefab, transform.position, Quaternion.identity);
        }

        // Destroy the key after it's collected
        Destroy(gameObject);
    }
}