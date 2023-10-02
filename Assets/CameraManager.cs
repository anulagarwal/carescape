using UnityEngine;

[ExecuteInEditMode]
public class CameraManager : MonoBehaviour
{
    public Camera portraitCamera;
    public Camera landscapeCamera;

    private void Update()
    {
        // Check orientation and switch cameras accordingly
        SwitchCameraBasedOnOrientation();
    }

    void SwitchCameraBasedOnOrientation()
    {
        // You might need to tweak this part to better simulate your target device's screen dimensions in the editor
        if (Screen.width > Screen.height || (Screen.width > 800 && Screen.height <= 600))
        {
            // Landscape mode
            EnableLandscapeCamera();
        }
        else
        {
            // Portrait mode
            EnablePortraitCamera();
        }
    }

    void EnableLandscapeCamera()
    {
        landscapeCamera.gameObject.SetActive(true);
        portraitCamera.gameObject.SetActive(false);
    }

    void EnablePortraitCamera()
    {
        portraitCamera.gameObject.SetActive(true);
        landscapeCamera.gameObject.SetActive(false);
    }
}
