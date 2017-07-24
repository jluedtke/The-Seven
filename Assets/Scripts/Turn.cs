using System.Collections;
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
        if (myTurn && !startTurnInitiated)
        {
            Debug.Log("Boop");
            StartTurn();
            startTurnInitiated = true;
        }
    }

	void StartTurn () {
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
        if (!myTurn)
        {
            return;
        }
        myTurn = false;
        startTurnInitiated = false;
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
