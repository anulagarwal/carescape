using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CarManager : MonoBehaviour
{
    public List<Car> cars;
    public List<Block> blocks;

    public int carUnlock;
    [SerializeField] List<Sprite> carImages;

    #region Singleton
    private static CarManager _instance;
    public static CarManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CarManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("CarManager");
                    _instance = singleton.AddComponent<CarManager>();
                }
            }
            return _instance;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        carUnlock = PlayerPrefs.GetInt("carunlock", 0);
        UpdateCars(carUnlock);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     // Add this function to add all BlockScript objects in the scene
    public void AddAllCarsInScene()
    {
        cars.Clear();
        Car[] blocksInScene = FindObjectsOfType<Car>();
        cars.AddRange(blocksInScene);
    }
    
    public int GetNextSkinImage()
    {
      return  carUnlock + 1;       
    }
    public void UpdateCarDirection()
    {
        AddAllCarsInScene();
        foreach(Car c in cars)
        {
            c.UpdatePattern();
        }
    }
    public void UpdateCars(int unlockLevel)
    {
        foreach(Car c in cars)
        {
            c.EnableSkin(unlockLevel);
        }
    }

    public void DisableCars()
    {
        foreach(Car c in cars)
        {
            c.GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void UnlockSpecialCar()
    {
        carUnlock++;
        PlayerPrefs.SetInt("carunlock", carUnlock);
        UpdateCars(carUnlock);
    }

    public void CheckForWin()
    {
        bool isWin = true;
        foreach(Car c in cars)
        {
            if(c.isFinished == false)
            {
                isWin = false;
                print("False");
                break;
            }
        }
        if (isWin)
        {
            GameManager.Instance.WinLevel();
        }
    }
}
