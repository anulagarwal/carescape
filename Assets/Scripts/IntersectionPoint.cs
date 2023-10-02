using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionPoint : MonoBehaviour
{
    [SerializeField] IntersectionPoint north;
    [SerializeField] IntersectionPoint south;
    [SerializeField] IntersectionPoint east;
    [SerializeField] IntersectionPoint west;
    [SerializeField] public bool right = true;
    [SerializeField] public bool left = true;
    [SerializeField] public bool up = true;
    [SerializeField] public bool down = true;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetNextPoint(CarDirection cd)
    {

    }
}
