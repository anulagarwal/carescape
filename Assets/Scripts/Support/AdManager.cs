using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CrazyGames;
//using AppodealAds.Unity.Api;
//using AppodealAds.Unity.Common;
//using CrazyGames;

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
       /* GameDistribution.OnResumeGame += OnResumeGame;
        GameDistribution.OnPauseGame += OnPauseGame;
        GameDistribution.OnPreloadRewardedVideo += OnPreloadRewardedVideo;
        GameDistribution.OnRewardedVideoSuccess += OnRewardedVideoSuccess;
        GameDistribution.OnRewardedVideoFailure += OnRewardedVideoFailure;
        GameDistribution.OnRewardGame += OnRewardGame;

        PreloadRewardedAd();*/
    }

    public void PreloadRewardedAd()
    {
      //  GameDistribution.Instance.PreloadRewardedAd();
    }

    public void ShowNormalAd(int id)    
    {
        currentId = id;
         //CrazyAds.Instance.beginAdBreak(ShowRewardedCallBack);
        GameManager.Instance.PauseGame();

        ShowRewardedCallBack();

      //  GameDistribution.Instance.ShowAd();

    }

    public void ShowLevelChangeAd()
    {
        // CrazyAds.Instance.beginAdBreak(ChangeLevel);
        ChangeLevel();
    }

    public void ChangeLevel()
    {
        GameManager.Instance.DoChangeLevel();
    }

 public void ShowRewarded(int id)
    {
        currentId = id;
        GameManager.Instance.PauseGame();

        //GameDistribution.Instance.ShowRewardedAd();
        ShowRewardedCallBack();
        //  CrazyAds.Instance.beginAdBreakRewarded(ShowRewardedCallBack);
        //Call rewarded show
    }

    void ShowRewardedCallBack()
    {
        GameManager.Instance.UnPauseGame();
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

            //Main Menu Ad
            case 4:
                CarManager.Instance.EnableCars();
                UIManager.Instance.DisableBossMenu(false);
                break;
            case 5:
                GameManager.Instance.DoChangeLevel();
                break;

        }
        CarManager.Instance.EnableCars();

    }



    public void OnResumeGame()
    {
        // RESUME MY GAME
        GameManager.Instance.UnPauseGame();

        switch (currentId)
        {
            case 5:
                ChangeLevel();
                break;
        }
    }

    public void OnPauseGame()
    {
        GameManager.Instance.PauseGame();

        // PAUSE MY GAME
    }

    public void OnRewardGame()
    {
        // REWARD PLAYER HERE
        GameManager.Instance.UnPauseGame();
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

    public void OnRewardedVideoSuccess()
    {
        // Rewarded video succeeded/completed.;
    }

    public void OnRewardedVideoFailure()
    {
        // Rewarded video failed.;
    }

    public void OnPreloadRewardedVideo(int loaded)
    {
        // Feedback about preloading ad after called GameDistribution.Instance.PreloadRewardedAd
        // 0: SDK couldn't preload ad
        // 1: SDK preloaded ad
    }
    
}
