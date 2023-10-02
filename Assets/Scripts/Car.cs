using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using DG.Tweening;
public class Car : MonoBehaviour
{
    [SerializeField] public CarPattern cp;
    [SerializeField] public float speed;
    [SerializeField] List<GameObject> patternObject;
    [SerializeField] int patternIndex;
    [SerializeField] int maxIndex;
    [SerializeField] bool isMoving;
    [SerializeField] bool isReverse;
    private float lastBacktrackTime = 0.0f;
    public float backtrackInterval = 1f;
   [SerializeField] private int currentStepIndex = -1;
    [SerializeField] CarDirection cd;
    public float rotationSpeed = 1.0f; // Adjust this speed as needed
   [SerializeField] private List<MovementStep> movementSteps = new List<MovementStep>();
    [SerializeField] List<GameObject> ps;
    [SerializeField] ParticleSystem hitVFX;

    [SerializeField] public List<GameObject> carSkins;

    public float shakeDuration = 0.5f;
    public float shakeStrength = 0.1f;
    public bool isFinished;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePattern();
        RecordMovementStep();
        speed = speed * 1.5f;
    }

    private void OnMouseDown()
    {
        if (TutorialManager.Instance != null)
        {
            if (GetComponent<TutorialStep>() != null)
            {

                if(TutorialManager.Instance.currentStep== GetComponent<TutorialStep>().ID)
                {
                    TutorialManager.Instance.PlayStep();
                    Move();

                }

            }
        }
        else
        {
            Move();
        }
    }


    
    public void EnableSkin(int index)
    {
        foreach(GameObject g in carSkins)
        {
            g.SetActive(false);
        }
        carSkins[index].SetActive(true);

    }

    void Move()
    {
        RecordMovementStep();
        GameManager.Instance.DoMove();
        isMoving = true;
        SoundManager.Instance.Play(SoundType.Drive);
        foreach(GameObject g in ps)
        {
            g.SetActive(true);
        }
    }
    public void UpdatePattern()
    {
        patternIndex = 0;
        foreach(GameObject g in patternObject)
        {
            g.SetActive(false);
        }

        switch (cp)
        {
            case CarPattern.Straight:
                maxIndex = 0;
                patternObject[0].SetActive(true);
                break;

            case CarPattern.Right:
                patternObject[1].SetActive(true);

                maxIndex = 1;
                break;
            case CarPattern.Left:
                patternObject[2].SetActive(true);

                maxIndex = 1;
                break;

            case CarPattern.UturnRight:
                patternObject[3].SetActive(true);

                maxIndex = 2;               
                break;
            case CarPattern.UturnLeft:
                patternObject[4].SetActive(true);

                maxIndex = 2;
                break;
        }

        // Get the current rotation in degrees
        Vector3 rotation = transform.eulerAngles;
        GetCardinalDirection(rotation.y);
        // Determine the cardinal direction based on the Y-axis rotation
    }

    void GetCardinalDirection(float rotationY)
    {
        // Ensure the rotation is within 0 to 360 degrees
        rotationY = (rotationY % 360 + 360) % 360;

        if (rotationY ==0 )
        {
            cd = CarDirection.South;
        }
        else if (rotationY ==90)
        {
            cd = CarDirection.East;

        }
        else if (rotationY ==180)
        {
            cd = CarDirection.North;
                    }
        else
        {
            cd = CarDirection.West;
        }
    }

    void RecordMovementStep()
    {
        // Create a MovementStep instance and add it to the list
        MovementStep step = new MovementStep(transform.position, transform.rotation);
        movementSteps.Add(step);
    }

    void MoveCar(CarDirection direction)
    {
        // Calculate the movement vector based on the direction
        Vector3 movement = Vector3.zero;

        switch (direction)
        {
            case CarDirection.North:
                movement = Vector3.forward;
                break;
            case CarDirection.South:
                movement = Vector3.back;
                break;
            case CarDirection.West:
                movement = Vector3.left;
                break;
            case CarDirection.East:
                movement = Vector3.right;
                break;
        }
        // Translate the car's position based on the movement vector and speed
        transform.Translate(movement * speed * Time.deltaTime);
        // Record the movement step
        RecordMovementStep();
    }



    // Update is called once per frame
    void Update()
    {
        if (isMoving && !isReverse)
        {
            //MoveCar(cd);
            transform.Translate(new Vector3(0,0,-1) * speed * Time.deltaTime);
            RecordMovementStep();

        }

        if (isMoving)
        {
            int i = PlayerPrefs.GetInt("carunlock", 0);
            if (!carSkins[i].GetComponent<MeshRenderer>().isVisible)
            {
                isMoving = false;
                isFinished = true;

                CarManager.Instance.CheckForWin();
                gameObject.SetActive(false);
            }
        }

        // Automatic backtracking logic
        if (Time.time - lastBacktrackTime >= backtrackInterval && isReverse)
        {
            lastBacktrackTime = Time.time;
            Backtrack();
        }
    }

    public bool CheckForPoint(IntersectionPoint ip, float currentAngle, Direction angle)
    {       

        if ( Mathf.Abs(currentAngle) <=5)
        {
            if(angle == Direction.Left)
            {

                return ip.right;
            }
            if(angle == Direction.Right)
            {
                return ip.left;
            }
        }
        else if (Mathf.Abs(currentAngle-90) <=5) 
        {
            if (angle == Direction.Left)
            {
                return ip.down;
            }
            if (angle == Direction.Right)
            {
                return ip.up;
            }

        }
        else if (Mathf.Abs(currentAngle - 180)<=5)
        {
            if (angle == Direction.Left)
            {
                return ip.left;
            }
            if (angle == Direction.Right)
            {
                return ip.right;
            }

        }
        else
        {
            if (angle == Direction.Left)
            {
                return ip.up;
            }
            if (angle == Direction.Right)
            {
                return ip.down;
            }
        }

        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "IntersectionPoint" && !isReverse && isMoving)
        {
            if (patternIndex < maxIndex)
            {

                switch (cp)
                {
                    case CarPattern.Straight:
                        maxIndex = 0;
                        break;

                    case CarPattern.Right:

                        if (CheckForPoint(other.GetComponent<IntersectionPoint>(), transform.eulerAngles.y, Direction.Right))
                        {
                            RotateCar(90.0f);
                            patternIndex++;
                        }
                        break;
                    case CarPattern.Left:
                        if (CheckForPoint(other.GetComponent<IntersectionPoint>(), transform.eulerAngles.y, Direction.Left))
                        {
                            RotateCar(-90.0f);
                            patternIndex++;
                        }
                        break;

                    case CarPattern.UturnRight:
                        if (CheckForPoint(other.GetComponent<IntersectionPoint>(), transform.eulerAngles.y, Direction.Right))
                        {
                            RotateCar(90.0f);
                            patternIndex++;
                        }
                        break;

                    case CarPattern.UturnLeft:
                        if (CheckForPoint(other.GetComponent<IntersectionPoint>(), transform.eulerAngles.y, Direction.Left))
                        {
                            RotateCar(-90.0f);
                            patternIndex++;
                        }
                        break;
                }
            }
           
        }
        if (other.gameObject.tag == "Wall")
        {
            isFinished = true;

            CarManager.Instance.CheckForWin();
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Car" && isMoving)
        {
            SoundManager.Instance.Play(SoundType.Crash);
            isReverse = true;
            isMoving = false;
            currentStepIndex = movementSteps.Count;
            collision.transform.DOShakePosition(shakeDuration, shakeStrength);
            hitVFX.transform.position = collision.contacts[0].point;
            hitVFX.Play();
            Backtrack();

            if(GameManager.Instance.numberOfMoves == 0)
            {
                GameManager.Instance.LoseLevel();
            }
        }
    }
    void RotateCar(float angle)
    {
        // Calculate the new rotation angle
        Vector3 newRotation = transform.eulerAngles + new Vector3(0.0f, angle, 0.0f);

        // Use DOTween to smoothly rotate the car
        transform.DORotate(newRotation, rotationSpeed, RotateMode.Fast);
        // Record the movement step
        RecordMovementStep();
    }

    void Backtrack()
    {
        // Check if there are recorded movement steps
        if (currentStepIndex-1<0)
        {
            isReverse = false;
            patternIndex = 0;
            movementSteps.Clear();
            foreach (GameObject g in ps)
            {
                g.SetActive(false);
            }
            Debug.LogWarning("No recorded movements to backtrack.");
            return;
        }
        Vector3 relativePositionChange = movementSteps[currentStepIndex-1].position - transform.position;
        Quaternion relativeRotationChange = Quaternion.Inverse(transform.rotation) * movementSteps[currentStepIndex-1].rotation;
        // Apply the relative changes to the car's position and rotation
        transform.position += relativePositionChange;
        transform.rotation *= relativeRotationChange;
        currentStepIndex--;

       
    }


}
