using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Movement : MonoBehaviour {

    public LayerMask blockingLayer;
    public float moveTime = 1;


    // Use this for initialization
    void Start()
    {
        InvokeRepeating("AttemptMove", 0f, .1f); //change or find a better way

    }

    private void AttemptMove()
    {
        if (GameManager.instance.playerTurn == false)
        {
            return;
        }

        Vector3 targetVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), -1);

        if (targetVelocity.x == 0 && targetVelocity.y == 0)
        {
            return;
        }
        else
            Move(targetVelocity);


    }

    private void Move(Vector3 targetVelocity)
    {
        Debug.Log(targetVelocity);
        if (GameManager.instance.playerTurn == false)
        {
            return;
        }
        else
        {
            GameManager.instance.playerTurn = false;

            Vector3 position = this.transform.position;
            position.x += targetVelocity.x;
            position.y += targetVelocity.y;

            this.transform.position = position;

        }

    // if (Object.instance.playerTurn = false) { playerTurn = true; return; }
    // Move once per turn. Or a certain amount of spaces defined in Player Object
    // Object.instance.playerTurn = false;
    }
}
