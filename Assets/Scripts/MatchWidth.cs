using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MatchWidth : MonoBehaviour
{
    public float sceneWidth = 10;  // Desired width in units
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
        // If the camera is not orthographic, this line will convert it
        _camera.orthographic = true;

        // Calculate the new orthographic size based on the scene width and screen aspect ratio
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        _camera.orthographicSize = sceneWidth / aspectRatio / 2.0f;
    }

    void Update()
    {

    }
}
