using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraZoneTrigger : MonoBehaviour
{
    public string cameraID; // e.g., "Front", "Back", "Top", etc.
    private UICubeManager uiCubeManager;

    private void Start()
    {
        uiCubeManager = FindObjectOfType<UICubeManager>();
        if (uiCubeManager == null)
        {
            Debug.LogError("UICubeManager not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraControll cameraController = FindObjectOfType<CameraControll>();
            if (cameraController != null)
            {
                cameraController.SwitchCamera(cameraID);
            }

            if (uiCubeManager != null)
            {
                uiCubeManager.ShowSurface(cameraID); // Update UI cube
            }
        }
    }
}