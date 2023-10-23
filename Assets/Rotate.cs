using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;



public class Rotate : MonoBehaviour
{
    public float duration = 1f; // Duration for one complete rotation
    public Vector3 rotationAngle = new Vector3(0, 0,360); // The angles to rotate to
    public int loops = -1; // Number of loops (-1 means infinite loops)
    public LoopType loopType = LoopType.Yoyo; // The type of loop to use

    // Start is called before the first frame update
    void Start()
    {
        // Rotate the GameObject and loop the animation
        transform.DORotate(rotationAngle, duration, RotateMode.FastBeyond360)
            .SetLoops(loops, loopType)
            .SetEase(Ease.Linear); // Constant rotation speed
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
