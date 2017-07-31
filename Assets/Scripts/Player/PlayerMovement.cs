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
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    public Vector2 input;
    public bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;

    public float counter;
    public float moveRange; //Default, PlayerStats changes it inside of PlayerStats script.
    public bool coroutineDone = true;

    public SpriteChanger spriteChanger;

    private LayerMask finalMask = (1 << 10) | (1 << 11);
    public PlayerActions pActions;

    void Start()
    {
        spriteChanger = GetComponentInChildren<SpriteChanger>();
        pActions = GetComponent<PlayerActions>();
        counter = 0f;
    }

    public void Update()
    {
        if (!GetComponent<Turn>().myTurn || !pActions.allEnemiesFound)
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
                if (counter < moveRange)
                {
                    StartCoroutine(Move(transform));
                }
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
            if (counter + 2 > moveRange)
            {
                isMoving = false;
                coroutineDone = true;
                StopAllCoroutines();

                yield return null;
            }
            factor = 0.7071f;
            yield return null;
        }
        else
        {
            factor = 1f;
        }

        if (counter > moveRange)
        {
            isMoving = false;
            StopAllCoroutines();
        }

        counter++;


        while (t < 1f)
        {
            spriteChanger.ChangeSprite(input);
            t += Time.deltaTime * (moveSpeed / gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }


        isMoving = false;
        coroutineDone = true;
        StopAllCoroutines();
        yield return 0;
    }
}
