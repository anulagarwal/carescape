using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonPool : MonoBehaviour
{
    public List<GameObject> pooledButtons;

    public GameObject buttonPrefab;

    public int pooledAmount = 20;

    void Start()
    {
        pooledButtons = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(buttonPrefab);
            obj.transform.SetParent(transform);

            obj.SetActive(false);
            pooledButtons.Add(obj);
        }
    }
    public GameObject GetPooledButton()
    {
        for (int i = 0; i < pooledButtons.Count; i++)
        {
            if (!pooledButtons[i].activeInHierarchy)
            {
                return pooledButtons[i];
            }
        }

        GameObject obj = Instantiate(buttonPrefab);
        obj.SetActive(false);
        pooledButtons.Add(obj);
        return obj;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
