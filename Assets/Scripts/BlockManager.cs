using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public List<Block> allBlocks = new List<Block>();
    [SerializeField] Vector2 blockOffset;
    [SerializeField] Vector2 gridSize;
    [SerializeField] GameObject blockPrefab;

    public Transform parentOfGrassBlocks; // Drag the parent GameObject here in the Inspector
    public float proximityThreshold = 0.1f; // How close should two blocks be to be considered "neighbors"


    private void Start()
    {
        RemoveWalls();
    }

    // Add this function to add all BlockScript objects in the scene
    public void AddAllBlocksInScene()
    {
        Block[] blocksInScene = FindObjectsOfType<Block>();
        allBlocks.AddRange(blocksInScene);
    }

    public void RemoveWalls()
    {
        // Create a list to hold the grass blocks
        List<GameObject> allGrassBlocks = new List<GameObject>();

        // Loop through each child of the parent GameObject and add it to the list
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Grass")) // Only consider GameObjects that start with the name "Grass"
            {
                allGrassBlocks.Add(child.gameObject);
            }
        }

        // Convert to array for easier looping
        GameObject[] allGrassBlocksArray = allGrassBlocks.ToArray();

        // Loop through each grass block and check its neighbors
        foreach (GameObject block in allGrassBlocksArray)
        {
            CheckAndDisableWalls(block, allGrassBlocksArray);
        }
    }

    public void UpdateList()
    {
    }

    void CheckAndDisableWalls(GameObject block, GameObject[] allBlocks)
    {
        Transform thisBlockTransform = block.transform;

        // Get the child walls
        Transform topWall = thisBlockTransform.GetChild(1);
        Transform bottomWall = thisBlockTransform.GetChild(0);
        Transform leftWall = thisBlockTransform.GetChild(3);
        Transform rightWall = thisBlockTransform.GetChild(2);

        // Enable all walls initially
        topWall.gameObject.SetActive(true);
        bottomWall.gameObject.SetActive(true);
        leftWall.gameObject.SetActive(true);
        rightWall.gameObject.SetActive(true);

        foreach (GameObject otherBlock in allBlocks)
        {
            if (otherBlock == block) continue; // Skip self

            Transform otherBlockTransform = otherBlock.transform;
            Vector3 offset = otherBlockTransform.position - thisBlockTransform.position;

            // Check if the blocks are directly next to each other (let's say 1.0f units apart)
            if (Mathf.Abs(offset.x) == 2.0f && offset.z == 0.0f)
            {
                if (offset.x > 0) rightWall.gameObject.SetActive(false);
                else leftWall.gameObject.SetActive(false);
            }
            else if (Mathf.Abs(offset.z) == 2.0f && offset.x == 0.0f)
            {
                if (offset.z > 0) topWall.gameObject.SetActive(false);
                else bottomWall.gameObject.SetActive(false);
            }
        }
    }



    public void RemoveAllBlocks()
    {
        foreach(Block b in allBlocks)
        {
            Destroy(b.gameObject);
        }
        allBlocks.Clear();
    }

    public void UpdateBlocks()
    {
        foreach (Block b in allBlocks)
        {
            b.UpdateBlock();
        }
    }

    // Add this function to resize the grid of blocks
    public void ResizeBlockGrid()
    {      
        for (int i = 0; i < allBlocks.Count; i++)
        {
            int x = i % (int)gridSize.x;
            int y = i / (int)gridSize.x;

            Vector3 newPosition = new Vector3(x * blockOffset.x, 0f, y * blockOffset.y);
            allBlocks[i].transform.position = newPosition;
        }
    }

    public void SpawnBlockGrid()
    {
        RemoveAllBlocks();
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 spawnPosition = new Vector3(x * blockOffset.x, 0f, y * blockOffset.y);
                GameObject newBlock = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
                newBlock.transform.SetParent(transform);
                Block blockScript = newBlock.GetComponent<Block>();

                if (blockScript != null)
                {
                    allBlocks.Add(blockScript);
                }
            }
        }

        RemoveWalls();
    }
   
}
