using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CustomAnimator : MonoBehaviour
{
    [SerializeField] Transform smiley;
    [SerializeField] Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        smiley.DOPunchScale(scale, 1.5f).SetLoops(-1,LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
