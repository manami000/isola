using UnityEngine;

public class CameraToggler : MonoBehaviour
{
    public GameObject extCam, intCam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            extCam.SetActive(!extCam.activeSelf);
            intCam.SetActive(!intCam.activeSelf);
        }
    }
}
