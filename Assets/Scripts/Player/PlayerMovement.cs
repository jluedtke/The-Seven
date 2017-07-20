using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public float moveSpeed = 3f;
    private float gridSize = 1f;
    private enum Orientation
    {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Horizontal;
    private bool allowDiagonals = true;
    private bool correctDiagonalSpeed = true;
    public Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;

    private GameObject gameManager;

    private float counter = 0f;
    public float moveRange = 5f; //Default, PlayerStats changes it.
    private bool coroutineDone = true;

    public SpriteChanger spriteChanger;

    private LayerMask finalMask = (1 << 10) | (1 << 11);


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        spriteChanger = GetComponentInChildren<SpriteChanger>();
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

            if (input != Vector2.zero && coroutineDone)
            {
                coroutineDone = false;
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


        RaycastHit2D hit = Physics2D.Linecast(startPosition, endPosition, finalMask); //Enemy Layer && wall layer
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Enemy") //Should equal enemy or something
            {
                isMoving = false;
                coroutineDone = true;
                StopAllCoroutines();
            }

            if (hit.collider.tag == "Wall")
            {
                isMoving = false;
                coroutineDone = true;
                StopAllCoroutines();
            }
            yield return null;
        }


        if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0)
        {
            if (counter + 2 >= moveRange)
            {
                isMoving = false;
                coroutineDone = true;
                StopAllCoroutines();
            }
            counter++;
            factor = 0.7071f;
            yield return null;
        }
        else
        {
            factor = 1f;
        }

        if (counter == moveRange)
        {
            isMoving = false;
            StopAllCoroutines();
        }

        while (t < 1f)
        {
            spriteChanger.ChangeSprite(input);
            t += Time.deltaTime * (moveSpeed / gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        counter++; //Should be on game manager
        //should allow for multiple moves per turn. 
        //Need to put on parent class and call from there

        if (counter >= moveRange)
        {
            counter = 0;
            isMoving = false;
            yield return new WaitForSeconds(2f); //Throw this on the turn caller
            gameManager.GetComponent<GameManager>().playerTurn = false; //turn to true when testing Player Movement
            coroutineDone = true;
            StopAllCoroutines();
        }

        isMoving = false;
        coroutineDone = true;
        yield return 0;
    }
}
