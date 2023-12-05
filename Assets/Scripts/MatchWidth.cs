using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MatchWidth : MonoBehaviour
{
    public float sceneWidth = 10;  // Desired width in units at a specific distance
    public float distanceFromCamera = 10;  // Distance from the camera to the point where the width is measured
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();

        AdjustFOV();
    }

    void Update()
    {
#if UNITY_EDITOR
        AdjustFOV();
#endif
    }

    void AdjustFOV()
    {
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        _camera.fieldOfView = 2.0f * Mathf.Atan(sceneWidth / (2.0f * aspectRatio * _camera.nearClipPlane)) * Mathf.Rad2Deg;

    }
}
