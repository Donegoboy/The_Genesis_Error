using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Camera3;
    public GameObject Camera4;
    public GameObject Camera5;
    public GameObject Camera6;

    private GameObject activeCamera;
    private Quaternion targetRotation;

    void Start()
    {
        Camera1.SetActive(true);
        activeCamera = Camera1;
        Camera2.SetActive(false);
        Camera3.SetActive(false);
        Camera4.SetActive(false);
        Camera5.SetActive(false);
        Camera6.SetActive(false);
    }

    public void SwitchCamera(string cameraID)
    {    
        if (activeCamera != null)
        {
            activeCamera.SetActive(false);
        }

        switch (cameraID)
        {
            case "CameraA":
                Camera1.SetActive(true);
                activeCamera = Camera1;
                break;
            case "CameraB":
                Camera2.SetActive(true);
                activeCamera = Camera2;
                break;
            case "CameraC":
                Camera3.SetActive(true);
                activeCamera = Camera3;
                break;
            case "CameraD":
                Camera4.SetActive(true);
                activeCamera = Camera4;
                break;
            case "CameraE":
                Camera5.SetActive(true);
                activeCamera = Camera5;
                break;
            case "CameraF":
                Camera6.SetActive(true);
                activeCamera = Camera6;
                break;
            default:
                Debug.LogWarning("Buraz, nema kamere: " + cameraID);
                break;
        }
    }
}