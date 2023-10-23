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

    [SerializeField] private float maxScroll;  // Maximum allowed scroll up from bottom
    [SerializeField] private float minScroll;  // Minimum allowed scroll down from top

    public void SpawnButton(int levelNumber)
    {
        GameObject button = buttonPool.GetPooledButton();
        button.transform.SetParent(contentTransform);
        button.transform.localPosition = new Vector3(0, (-currentLevelCount * buttonSpacing) + (totalLevels * buttonSpacing), 0);

        // Set the level number
        TextMeshProUGUI levelText = button.GetComponentInChildren<TextMeshProUGUI>(); // or TextMeshProUGUI if you're using TextMeshPro
        levelText.text = levelNumber.ToString();

        button.SetActive(true);

        // Increment the current level count
        currentLevelCount++;
    }
    RectTransform contentRectTransform;

    void Start()
    {
       

        // Set content size
        float contentHeight = totalLevels * buttonSpacing;
        contentRectTransform = contentTransform.GetComponent<RectTransform>();
       // contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, contentHeight);

        // Start with the highest level at the center
        float initialOffset = scrollRect.viewport.rect.height / 2;
        //contentRectTransform.anchoredPosition = new Vector2(0, -initialOffset);

       // currentLevelCount = totalLevels;

        for (int i = totalLevels; i >= 1; i--)
        {
            SpawnButton(i);
        }

        // Define the limits
        float viewportHeight = scrollRect.viewport.rect.height;
        maxScroll = contentRectTransform.sizeDelta.y - (viewportHeight / 2);  // Half of viewport to center the last button
        minScroll = viewportHeight / 2;  // Half of viewport to center the first button

    }


   

    // Update is called once per frame
    void Update()
    {

        float currentY = contentRectTransform.anchoredPosition.y;
        print(currentY);
        if (currentY > maxScroll)
        {
            //contentRectTransform.anchoredPosition = new Vector2(0, maxScroll);
        }
        else if (currentY < minScroll)
        {
            //contentRectTransform.anchoredPosition = new Vector2(0, minScroll);
        }

    }
}
