using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] BlockType t;
    [SerializeField] List<GameObject> blocks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateBlock()
    {
        UpdateType(t);
    }

    public void UpdateType(BlockType ty)
    {
        switch (ty)
        {
            case BlockType.Grass:
                blocks[0].SetActive(true);
                blocks[1].SetActive(false);

                break;

            case BlockType.Road:
                blocks[0].SetActive(false);
                blocks[1].SetActive(true);
                break;
        }
    }
}
