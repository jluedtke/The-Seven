using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 10f;
    public float damage = 1f;
    public float attackRange = 1f;
    public float moveRange = 1f;

    private Transform target;

    public LayerMask blockingLayer;
    public float moveTime = 1;


    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("AttemptMove", 0f, .1f); // change or find a better way
    }

    private void AttemptMove()
    {
        if (GameManager.instance.playerTurn == true)
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

        Vector3 targetVelocity = new Vector3(xDir, yDir, -1);


        Move(targetVelocity);


    }

    private void Move(Vector3 targetVelocity)
    {

        if (GameManager.instance.playerTurn == true)
        {
            return;
        }
        else
        {
            GameManager.instance.playerTurn = true;

            Vector3 position = this.transform.position;
            position.x += targetVelocity.x;
            position.y += targetVelocity.y;

            this.transform.position = position;

        }
    }
}
