using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class LevelButtonManager : MonoBehaviour
{
    public LevelButtonPool buttonPool;
    public Transform contentTransform;
    public float buttonSpacing = 100f;
    private int currentLevelCount = 0;
    public ScrollRect scrollRect;
    public int totalLevels = 100;
    public int currentLevel = 7;

    [SerializeField] private float maxScroll;  // Maximum allowed scroll up from bottom
    [SerializeField] private float minScroll;  // Minimum allowed scroll down from top
    [SerializeField] private List<int> levelData;
    RectTransform contentRectTransform;


    public void SpawnButton(int levelNumber)
    {
        GameObject button = buttonPool.GetPooledButton();
        button.transform.SetParent(contentTransform);
        button.transform.localPosition = new Vector3(0, ((-currentLevelCount - (currentLevel-1)) * buttonSpacing) + (totalLevels * buttonSpacing) -125, 0);

        // Set the level number
        TextMeshProUGUI levelText = button.GetComponentInChildren<TextMeshProUGUI>(); // or TextMeshProUGUI if you're using TextMeshPro
        levelText.text = levelNumber.ToString();

        button.SetActive(true);

        // Increment the current level count
        currentLevelCount++;
    }

    void Start()
    {
       
        // Set content size
        contentRectTransform = contentTransform.GetComponent<RectTransform>();
        // contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, contentHeight);

        // Start with the highest level at the center
        //contentRectTransform.anchoredPosition = new Vector2(0, -initialOffset);

        // currentLevelCount = totalLevels;
        currentLevel = PlayerPrefs.GetInt("level", 1);
        buttonPool.Spawn(currentLevel, totalLevels);

        for (int i = totalLevels; i >= 1; i--)
        {
            SpawnButton(i);
        }


        maxScroll = buttonSpacing * (currentLevel-1);
        minScroll = -(buttonSpacing * (totalLevels-5));
    }




    // Update is called once per frame
    void Update()
    {

        float currentY = contentRectTransform.anchoredPosition.y;
        if (currentY > maxScroll)
        {
           contentRectTransform.anchoredPosition = new Vector2(0, maxScroll);
        }
        else if (currentY < minScroll)
        {
            contentRectTransform.anchoredPosition = new Vector2(0, minScroll);
        }

    }
}
