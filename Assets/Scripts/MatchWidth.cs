using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MatchWidth : MonoBehaviour
{
    // Set this to the in-world distance between the left & right edges of your scene.
    public float sceneWidth = 10;

    Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographic = true; // Ensure the camera is Orthographic
    }

    // Adjust the camera's height so the desired scene width fits in view
    // even if the screen/window size changes dynamically.
    void Update()
    {
        // The aspect ratio is the ratio of the width of the window to the height.
        float aspectRatio = Screen.width / (float)Screen.height;

        // Camera orthographicSize is the size in world units that the camera will cover vertically.
        // To calculate the orthographic size such that certain width fits in the screen view
        // considering the aspect ratio we do the following
        _camera.orthographicSize = sceneWidth / (2.0f * aspectRatio);
    }
}
