using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    public List<GameObject> combatants;
    public List<GameObject> turnOrder;
    private GameObject[] enemies;
    private bool turnsSet = false;

	// Use this for initialization
	public void StartTurnOrder () {
        if (!(GameObject.FindGameObjectWithTag("Player")) || turnsSet)
        {
            return;
        }
        combatants.Add(GameObject.FindGameObjectWithTag("Player"));
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            combatants.Add(enemies[i]);
        }
        SetTurns();
    }

    void OnRenderObject()
    {
        StartTurnOrder();
    }
	
    void SetTurns()
    {
        if (turnsSet)
            return;

        turnOrder = combatants;    

        turnOrder.Sort((p1, p2) => p1.GetComponent<Stats>().init.CompareTo(p2.GetComponent<Stats>().init));

        turnOrder.Reverse();

        for (int i = 0; i < turnOrder.Count; i++)
        {
            Debug.Log(turnOrder[i].GetComponent<Stats>().init);
        }
        turnsSet = true;
        ManageTurn();
    }

    bool hasInit(List<GameObject> people)
    {
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].GetComponent<Stats>().init != 0)
                return true;
        }   

        return false;
    }

    void ManageTurn()
    {
        if (!turnOrder[0].GetComponent<Turn>().myTurn)
        {
            turnOrder[0].GetComponent<Turn>().myTurn = true;
        }
        else
        {
            turnOrder[0].GetComponent<Turn>().myTurn = false;
            turnOrder[0].GetComponent<Turn>().startTurnInitiated = false;

            passTurn();
        }

    }

    void passTurn()
    {
        GameObject tempObject = turnOrder[0];
        turnOrder.Remove(turnOrder[0]);
        turnOrder.Add(tempObject);
        ManageTurn();
    }
}
