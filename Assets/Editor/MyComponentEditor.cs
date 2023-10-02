using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Car))]
public class MyComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Car myComponent = (Car)target;

        // Draw the default Inspector GUI
        DrawDefaultInspector();

        // Add a button to the Inspector
        if (GUILayout.Button("Update"))
        {
            // Call the function when the button is pressed
            myComponent.UpdatePattern();
        }
    }
}
