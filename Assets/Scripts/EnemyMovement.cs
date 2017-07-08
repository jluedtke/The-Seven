using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private float gridSize = 1f;
    private enum Orientation
    {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Horizontal;
    public bool allowDiagonals = true;
    private bool correctDiagonalSpeed = true;
    public Vector2 input;
    public bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;

    private GameObject gameManager;
    private Transform target;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        target = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("AttemptMove", 0f, .1f); // call 10 per/sec

    }

    public void AttemptMove()
    {
        if (gameManager.GetComponent<GameManager>().playerTurn == true)
        {
            return;
        }


        int xDir = 0;
        int yDir = 0;

        if (Mathf.Abs(target.position.x - transform.position.x) < .1f)
        {
            yDir = target.position.y > transform.position.y ? 1 : -1;
        }
        else
        {
            xDir = target.position.x > transform.position.x ? 1 : -1;
        }


        if (!isMoving)
        {
            input = new Vector2(xDir, yDir);
            if (!allowDiagonals)
            {
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                {
                    input.y = 0;
                }
                else
                {
                    input.x = 0;
                }
            }

            if (input != Vector2.zero)
            {
                StartCoroutine(Move(transform));
            }
        }
    }

    public virtual IEnumerator Move(Transform transform)
    {
        isMoving = true;

        startPosition = transform.position;
        t = 0;

        if (gridOrientation == Orientation.Horizontal)
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        }
        else
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        }

        if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0)
        {
            factor = 0.7071f;
        }
        else
        {
            factor = 1f;
        }

        while (t < 1f)
        {
            t += Time.deltaTime * (moveSpeed / gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        isMoving = false;
        gameManager.GetComponent<GameManager>().playerTurn = true;
        yield return 0;
    }
}
