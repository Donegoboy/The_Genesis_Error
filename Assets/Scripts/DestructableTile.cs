using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleTile : MonoBehaviour
{

    public GameObject replacementTilePrefab; 
    public string replacementLayerName = "ReplacementLayerName";
    public AudioClip breakingSfx;

    public GameObject otherTile;

    private bool playerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && playerInside)
        {
            if (breakingSfx != null)
            {
                GameObject soundObject = new GameObject("BreakingSFX");
                soundObject.transform.position = transform.position;

                AudioSource audioSource = soundObject.AddComponent<AudioSource>();
                audioSource.clip = breakingSfx;
                audioSource.spatialBlend = 0f;
                //audioSource.pitch = 2.5f;
                audioSource.Play();
                Destroy(soundObject, audioSource.clip.length);
            }
            DestroyTile();
        }
    }

    private void DestroyTile()
    {

        if (replacementTilePrefab != null)
        {
            Debug.Log("Original tile position: " + transform.position);
            Vector3 offset = new Vector3(4.5f, -0.5f, 0f);

            GameObject newTile = Instantiate(replacementTilePrefab, transform.position + offset, transform.rotation);
            Debug.Log("New tile position: " + newTile.transform.position);

            SetLayerToNewTile(newTile, replacementLayerName);
        }
        if (otherTile != null)
        {
            Debug.Log("Destroying other tile at: " + otherTile.transform.position);
            Destroy(otherTile);
        }

        Destroy(gameObject);
    }

    private void SetLayerToNewTile(GameObject obj, string layerName)
    {
        int layerIndex = LayerMask.NameToLayer(layerName);
        if (layerIndex != -1)
        {
            obj.layer = layerIndex;
            foreach (Transform child in obj.transform)
            {
                SetLayerToNewTile(child.gameObject, layerName);
            }
        }
    }
}