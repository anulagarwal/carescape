using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using AppodealAds.Unity.Api;
//using AppodealAds.Unity.Common;
using CrazyGames;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance = null;


    bool isRewardedGD;

    public int currentId;

    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

    }

    private void Start()
    {
        
    }

    public void ShowNormalAd(int id)    
    {
        currentId = id;
        CrazyAds.Instance.beginAdBreak(ShowRewardedCallBack);
    }

    public void ShowLevelChangeAd()
    {
        CrazyAds.Instance.beginAdBreak(ChangeLevel);
    }

    public void ChangeLevel()
    {
        GameManager.Instance.DoChangeLevel();
    }

 public void ShowRewarded(int id)
    {
        currentId = id;
        CrazyAds.Instance.beginAdBreakRewarded(ShowRewardedCallBack);
        //Call rewarded show
    }

    void ShowRewardedCallBack()
    {
        switch (currentId)
        {
            //Skip Level
            case 0:
                GameManager.Instance.SkipLevel();
                break;

            //Get moves
            case 1:
                GameManager.Instance.GiveMovesAfterReward();
                break;

            //Unlock car special
            case 2:
                PlayerPrefs.SetInt("carunlock", CarManager.Instance.GetNextSkinImage());
                CarManager.Instance.UnlockSpecialCar();
                UIManager.Instance.specialCar.SetActive(false);
                break;

           //Main Menu Ad
            case 3:
                GetComponent<MainMenuHandler>().DoPlay();
                break;           

        }
    }

}
