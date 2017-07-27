using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private Turn pTurn;
    private PlayerMovement pMovement;
    private PlayerActions pAction;
    private Stats pStats;


	// Use this for initialization
	void Start () {
        pTurn = GetComponent<Turn>();
        pMovement = GetComponent<PlayerMovement>();
        pAction = GetComponent<PlayerActions>();
        pStats = GetComponent<Stats>();
    }

    void Update () {
        if (pAction.enemies.Count < 1)
        {
            pMovement.counter = 0;
            pTurn.myTurn = true;
        }
	}
}
