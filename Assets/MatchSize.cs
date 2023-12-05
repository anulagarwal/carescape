using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MatchSize : MonoBehaviour
{
    public float sceneHeight = 5.6f;  // Desired height in units for landscape
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();

        // If the camera is not orthographic, this line will convert it
        _camera.orthographic = true;
    }

    void Start()
    {
        AdjustCameraSize();
    }

    void Update()
    {
#if UNITY_EDITOR
        AdjustCameraSize();
#endif
    }

    void AdjustCameraSize()
    {
        // Calculate the new orthographic size based on the scene height and screen aspect ratio
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        _camera.orthographicSize = sceneHeight / 2.0f / aspectRatio;
    }
}
