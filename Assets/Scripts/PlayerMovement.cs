using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    public void Update()
    {
        if (gameManager.GetComponent<GameManager>().playerTurn == false)
        {
            return;
        }

        if (!isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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





        Vector2 raycastVector = new Vector2(endPosition.x + Input.GetAxisRaw("Horizontal"), endPosition.y + Input.GetAxisRaw("Vertical"));
        Vector2 startRaycastVector = new Vector2(startPosition.x, startPosition.y);


        RaycastHit2D hit = Physics2D.Linecast(startRaycastVector, raycastVector);
        Debug.DrawLine(startRaycastVector, raycastVector, Color.red);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.name != "Player")
            {
                Debug.Log("Hit");
                factor = 0;
                endPosition = startPosition;
                yield return 0;

            }

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
        gameManager.GetComponent<GameManager>().playerTurn = true; //turn back to false
        yield return 0;
    }
}
