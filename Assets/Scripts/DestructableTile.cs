using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleTile : MonoBehaviour
{
    public GameObject replacementTilePrefab; // Drag your replacement tile prefab here
    public string replacementLayerName = "YourReplacementLayerName"; // Name of the layer

    private bool playerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) { }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && playerInside)
        {
            DestroyTile();
        }
    }

    private void DestroyTile()
    {
        // ... (Optional: Visual/Sound effects) ...

        // Spawn the replacement tile
        if (replacementTilePrefab != null)
        {
            Debug.Log("Original tile position: " + transform.position);
            Vector3 offset = new Vector3(4.5f, -0.5f, 0f);

            GameObject newTile = Instantiate(replacementTilePrefab, transform.position + offset, transform.rotation);
            Debug.Log("New tile position: " + newTile.transform.position);

            // Get the layer index from the layer name
            int layerIndex = LayerMask.NameToLayer(replacementLayerName);

            if (layerIndex != -1)
            {
                newTile.layer = layerIndex;
            }
            else
            {
                Debug.LogError("Layer with name '" + replacementLayerName + "' not found!");
            }
        }

        Destroy(gameObject);
    }
}