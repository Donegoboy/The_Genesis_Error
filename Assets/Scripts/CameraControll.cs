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

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 720f; // Degrees per second

    private GameObject activeCamera;
    private Quaternion targetRotation;
    private bool isRotating = false;

    void Start()
    {
        // Deactivate all cameras except the first one 
        Camera1.SetActive(true);
        activeCamera = Camera1;
        Camera2.SetActive(false);
        Camera3.SetActive(false);
        Camera4.SetActive(false);
        Camera5.SetActive(false);
        Camera6.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RotateCameraClockwise90();
        }
    }

    public void SwitchCamera(string cameraID)
    {
        // Deactivate the currently active camera
        if (activeCamera != null)
        {
            activeCamera.SetActive(false);
        }

        // Activate the new camera based on the ID
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

    public void RotateCameraClockwise90()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCameraCoroutine(90f)); // Rotate 90 degrees clockwise
        }
    }

    private IEnumerator RotateCameraCoroutine(float angle)
    {
        isRotating = true;

        // Calculate target rotation based on current rotation and angle
        targetRotation = activeCamera.transform.rotation * Quaternion.Euler(0, 0, angle);

        // Smoothly rotate towards the target rotation
        while (Quaternion.Angle(activeCamera.transform.rotation, targetRotation) > 0.1f)
        {
            activeCamera.transform.rotation = Quaternion.RotateTowards(activeCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        activeCamera.transform.rotation = targetRotation; // Snap to exact rotation at the end
        isRotating = false;
    }
}