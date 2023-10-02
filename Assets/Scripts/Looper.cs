using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Looper : MonoBehaviour
{

    #region Singleton
    private static Looper _instance;
    public static Looper Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Looper>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("Looper");
                    _instance = singleton.AddComponent<Looper>();
                }
            }
            return _instance;
        }
    }
    #endregion
    public List<Vector2> vertices;
    public List<Vector2> offsetVertices;

    private int currentVertex = 0;
    public float speed = 1f; // How quickly the object should move from one vertex to another
    public bool isMoving = false; // Bool to control if the object should move
    public string stopTag = "Stop"; // The tag of the object that should stop this object

    public void HandleLoop(List<Vector2> vertices)
    {
        this.vertices = vertices; // Offset vertices based on current position
                                  //  this.offsetVertices = vertices;
        currentVertex = 1;
        isMoving = true; // Enable movement once the path is received
        offsetVertices.Clear();
        offsetVertices.Add(Vector2.zero);
        for (int i = 1; i < vertices.Count; i++)
        {
            offsetVertices.Add(vertices[i] - vertices[i - 1]);
        }
        transform.position = vertices[0];
    }

    void Update()
    {
        if (isMoving && vertices != null && vertices.Count > 0)
        {
            Vector3 targetPosition = vertices[currentVertex];
            // Vector3 targetPosition = offsetVertices[currentVertex];

            float step = speed * Time.deltaTime; // Calculate distance to move based on speed and time

            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            Vector3 direction = (targetPosition - transform.position).normalized;
            if (direction != Vector3.zero) // Prevents NaN when direction is (0,0,0)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            // Check if the position of the this object and target position are approximately equal
            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                // Cycle to next vertex
                currentVertex = (currentVertex + 1) % vertices.Count;
                if (currentVertex == 0)
                {
                    UpdatePoints();
                }
            }
        }
    }

    public void UpdatePoints()
    {
        vertices[0] = transform.position;
        for (int i = 1; i < vertices.Count; i++)
        {
            vertices[i] = vertices[i - 1] + offsetVertices[i];
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == stopTag) // Check if the object has the tag that should stop this object
        {
            isMoving = false;
        }
    }

    // Function to toggle movement
    public void ToggleMovement(bool shouldMove)
    {
        isMoving = shouldMove;
    }
}
