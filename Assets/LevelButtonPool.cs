using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonPool : MonoBehaviour
{
    public List<GameObject> pooledButtons;

    public GameObject buttonPrefab;
    public GameObject buttonPrefabCurrent;
    public GameObject buttonPrefabActivated;
    public GameObject buttonPrefabBoss;
    public GameObject dots;

    public List<int> bossLevels;


    public int pooledAmount = 20;

    void Start()
    {
        pooledButtons = new List<GameObject>();

        
    }

    public void Spawn(int currentLevel, int totalLevels)
    {

        for (int i = totalLevels; i > currentLevel; i--)
        {
            GameObject obj = Instantiate(buttonPrefab);
            obj.transform.SetParent(transform);

            obj.SetActive(false);
            pooledButtons.Add(obj);
        }



        GameObject obja = Instantiate(buttonPrefabCurrent);
        obja.transform.SetParent(transform);
        obja.GetComponent<Button>().onClick.AddListener(() => MainMenuHandler.Instance.Play());

        obja.SetActive(false);
        pooledButtons.Add(obja);
        for (int i = currentLevel - 1; i > 0; i--)
        {
            GameObject obj = Instantiate(buttonPrefabActivated);
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
                print("sadf");
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
