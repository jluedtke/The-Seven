using System.Collections;
using System.Collections.Generic;
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
    public bool allowDiagonals = false;
    public Vector2 input;
    public bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;

    public float counter = 0f;
    public float moveRange = 5f; //grabbed from stats
    private bool coroutineDone = true;

    public List<Node> path;

    private LayerMask finalMask = (1 << 9) | (1 << 11);

    private Turn thisTurn;

    // Use this for initialization
    void Start()
    {
        counter = 0f;
        thisTurn = GetComponent<Turn>();
    }

    public void DoMovement()
    {
         AttemptMove();
    }

    public void AttemptMove()
    {
        if (!coroutineDone || isMoving || !thisTurn.myTurn)
        {
            return;
        }

        float xDir = 0;
        float yDir = 0;

        xDir = path[0].worldPosition.x;
        yDir = path[0].worldPosition.y;

        if (GameObject.Find("Player").transform.position.x == xDir && GameObject.Find("Player").transform.position.y == yDir)
        {
            GetComponent<EnemyActions>().AttackPlayer();
            isMoving = false;
            coroutineDone = true;
            return;
        }


        if (!isMoving && counter < moveRange)
        {
            input = new Vector2(xDir, yDir);


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

        endPosition = input;


        RaycastHit2D hit = Physics2D.Linecast(startPosition, endPosition, finalMask); 
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Player")
            {
                isMoving = false;
                coroutineDone = true;
                GetComponent<EnemyActions>().AttackPlayer();
                StopAllCoroutines();
            }

            yield return null;

        }

        if (Mathf.Abs(startPosition.x - endPosition.x) == 1 && Mathf.Abs(startPosition.y - endPosition.y) == 1)
        {
            if (counter + 2 >= moveRange)
            {
                isMoving = false;
                coroutineDone = true;
                thisTurn.EndTurn();
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

        while (t < 1f)
        {
            t += Time.deltaTime * (moveSpeed / gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        counter++; 

        if (counter >= moveRange)
        {
            isMoving = false;
            coroutineDone = true;
            thisTurn.EndTurn();
            StopAllCoroutines();
        }

        isMoving = false;
        coroutineDone = true;
        AttemptMove();
        yield return 0;
    }
}
