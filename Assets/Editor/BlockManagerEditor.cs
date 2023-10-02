using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlockManager))]
public class BlockManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BlockManager blockManager = (BlockManager)target;

        if (GUILayout.Button("Add All Blocks in Scene"))
        {
            blockManager.AddAllBlocksInScene();
        }

        if (GUILayout.Button("Spawn Block Grid"))
        {          
            blockManager.SpawnBlockGrid();
        }

        if (GUILayout.Button("Resize"))
        {
            blockManager.ResizeBlockGrid();
        }
        if (GUILayout.Button("Update Blocks"))
        {
            blockManager.UpdateBlocks();
        }

        if (GUILayout.Button("Remove Walls"))
        {
            blockManager.RemoveWalls();
        }
    }
}
