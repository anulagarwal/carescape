using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CarManager))]
public class CarManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CarManager carManager = (CarManager)target;

        if (GUILayout.Button("Add All Blocks in Scene"))
        {
            carManager.UpdateCarDirection();
        }

       
    }
}
