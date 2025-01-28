using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICubeManager : MonoBehaviour
{
    public GameObject surfaceA;
    public GameObject surfaceB;
    public GameObject surfaceC;
    public GameObject surfaceD;
    public GameObject surfaceE;
    public GameObject surfaceF;

    public void ShowSurface(string surfaceName)
    {
        DisableAllCubes();
        switch (surfaceName)
        {
            case "CameraA":
                surfaceA.SetActive(true);
                break;
            case "CameraB":
                surfaceB.SetActive(true);
                break;
            case "CameraC":
                surfaceC.SetActive(true);
                break;
            case "CameraD":
                surfaceD.SetActive(true);
                break;
            case "CameraE":
                surfaceE.SetActive(true);
                break;
            case "CameraF":
                surfaceF.SetActive(true);
                break;
            default:
                Debug.LogError("Invalid face name: " + surfaceName);
                break;
        }
    }
    private void DisableAllCubes()
    {
        surfaceA.SetActive(false);
        surfaceB.SetActive(false);
        surfaceC.SetActive(false);
        surfaceD.SetActive(false);
        surfaceE.SetActive(false);
        surfaceF.SetActive(false);
    }
}
