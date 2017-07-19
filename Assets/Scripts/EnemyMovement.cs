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

    private GameObject gameManager;

    private float counter = 0f;
    private float moveRange = 5f; //Speed
    private bool coroutineDone = true;

    public List<Node> path;

    private LayerMask finalMask = (1 << 9) | (1 << 11);

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        InvokeRepeating("AttemptMove", 0f, .5f); // call 10 per/sec
    }

    public void AttemptMove()
    {
        if (!coroutineDone || isMoving || gameManager.GetComponent<GameManager>().playerTurn)
        {
            return;
        }

        float xDir = 0;
        float yDir = 0;

        xDir = path[0].worldPosition.x;
        yDir = path[0].worldPosition.y;


        if (!isMoving)
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


        RaycastHit2D hit = Physics2D.Linecast(startPosition, endPosition, finalMask); //FUCKING LAYERS DUDE!
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Player")
            {
                counter = 0;
                //tell to attack or something if melee
                Debug.Log(hit.collider.name);
                isMoving = false;
                coroutineDone = true;
                gameManager.GetComponent<GameManager>().playerTurn = true; // take out later, just use for melee only scripts.

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
                gameManager.GetComponent<GameManager>().playerTurn = true; // take out later, just use for melee only scripts.

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
            counter = 0;
            isMoving = false;
            // Do I need this?
            // yield return new WaitForSeconds(2f); 
            gameManager.GetComponent<GameManager>().playerTurn = true;
            coroutineDone = true;
            StopAllCoroutines();
        }

        
        isMoving = false;
        coroutineDone = true;
        yield return 0;
    }
}
