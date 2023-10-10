using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] int currentLevel;
    [SerializeField] TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("level", 1);
        levelText.text = "LEVEL " + currentLevel;
        AdManager.Instance.PreloadRewardedAd();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        AdManager.Instance.ShowNormalAd(5);
    }

    public void DoPlay()
    {
        SceneManager.LoadScene("Core");
    }
    public void OpenShoot()
    {
        Application.OpenURL("https://www.crazygames.com/game/shoot-bounce");
    }

    public void OpenYoutube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UChtVf9R8XKJnHiu9Hejn5Rg");

    }

    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/anulagarwal");
    }



}
