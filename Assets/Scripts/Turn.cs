﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {

    private PlayerMovement pMovement;
    private EnemyMovement eMovement;
    private PlayerActions pActions;
    private EnemyActions eActions;

    public bool myTurn;
    public bool startTurnInitiated;

    private void Start()
    {

        if (transform.gameObject.name == "Player")
        {
            pActions = GetComponent<PlayerActions>();
            pMovement = GetComponent<PlayerMovement>();
            return;
        }
        else
        {
            eActions = GetComponent<EnemyActions>();
            eMovement = GetComponent<EnemyMovement>();
        }
    }

    void Update()
    {
        StartTurn();
    }

	void StartTurn () {
        if (!myTurn || startTurnInitiated)
        {
            return;
        }
        if (myTurn)
        {
            startTurnInitiated = true;
        }

        if (transform.gameObject.name == "Player")
        {
            pMovement.counter = 0;
            pActions.mAction = 0;
            pActions.pAction = 0;
        }
        else
        {
            if (myTurn)
            {
                eMovement.counter = 0;
                eActions.mAction = 0;
                eActions.pAction = 0;
                eMovement.DoMovement();
            }
            return;
        }
    }

    public void EndTurn()
    {
        if (!myTurn && gameObject.name == "Player")
        {
            return;
        }
        myTurn = false;
        startTurnInitiated = false;

        if (transform.gameObject.name == "Player")
        {
            for (int i = 0; i < pActions.enemies.Length; i++)
            {
                if (pActions.enemies[i] != null)
                {
                    pActions.enemies[i].GetComponentInChildren<SpriteRenderer>().material.color = pActions.originalColor;
                }
            }
        }
        else
        {
            eMovement.counter = 0;
            eActions.mAction = 0;
            eActions.pAction = 0;
            print("Running");

        }

        GameObject.Find("GameManager").GetComponent<TurnManager>().NextTurn();
    }

    public void CheckForDeath()
    {
        if (GetComponent<Stats>().currentHP < 0)
        {
            Destroy(gameObject);
        }
    }

}