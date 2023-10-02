using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] int maxSteps = 3;
    [SerializeField] public int currentStep = 1;

    public static TutorialManager Instance = null;

    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        anim.Play("Step" + currentStep);
    }

   public void PlayStep()
    {
        currentStep++;
        if (currentStep > maxSteps)
        {
            gameObject.SetActive(false);
            return;
        }
        anim.Play("Step" + currentStep);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
