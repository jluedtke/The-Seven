using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {

    private PlayerMovement pMovement;
    private EnemyMovement eMovement;
    private PlayerActions pActions;
    private EnemyActions eActions;

    public bool myTurn = false;
    public bool startTurnInitiated = false;

    void Update()
    {
        if (myTurn == true && startTurnInitiated)
        {
            StartTurn();
            startTurnInitiated = true;
        }
    }

	void StartTurn () {
		if (transform.gameObject.name == "Player")
        {
            pActions = GetComponent<PlayerActions>();
            pMovement = GetComponent<PlayerMovement>();
            pMovement.enabled = true;
            pActions.enabled = true;
        } else
        {
            eActions = GetComponent<EnemyActions>();
            eMovement = GetComponent<EnemyMovement>();
            eMovement.enabled = true;
            eActions.enabled = true;
        }
    }

    public void EndTurn()
    {
        eActions.enabled = false;
        eMovement.enabled = false;
    }
	
}
