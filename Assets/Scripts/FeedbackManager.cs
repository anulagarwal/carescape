using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FeedbackManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject feedbackBox;
    [SerializeField] GameObject socialBox;

    [Header("FeedbackButtons")]
    [SerializeField] Button easyBtn;
    [SerializeField] Button boringBtn;
    [SerializeField] Button amazingBtn;
    [SerializeField] Button difficultBtn;
    [SerializeField] Sprite selectedImg;
    [SerializeField] Sprite otherImg;


    [Header("Feedback Data")]
    [SerializeField] bool isSelected;
    [SerializeField] Button activeButton;
    [SerializeField] int activeButtonID;







    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateButtonState(int i)
    {
        switch (i)
        {

            case 1:
                easyBtn.image.sprite = selectedImg;
                boringBtn.image.sprite = otherImg;
                amazingBtn.image.sprite = otherImg;
                difficultBtn.image.sprite = otherImg;
                break;

            case 2:
                easyBtn.image.sprite = otherImg;
                boringBtn.image.sprite = selectedImg;
                amazingBtn.image.sprite = otherImg;
                difficultBtn.image.sprite = otherImg;
                break;

            case 3:
                easyBtn.image.sprite = otherImg;
                boringBtn.image.sprite = otherImg;
                amazingBtn.image.sprite = selectedImg;
                difficultBtn.image.sprite = otherImg;
                break;

            case 4:
                easyBtn.image.sprite = otherImg;
                boringBtn.image.sprite = otherImg;
                amazingBtn.image.sprite = otherImg;
                difficultBtn.image.sprite = selectedImg;
                break;

        }
    }
}
