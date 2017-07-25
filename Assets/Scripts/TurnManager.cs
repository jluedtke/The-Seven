using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    public List<GameObject> combatants;
    public List<GameObject> turnOrder;
    private GameObject[] enemies;
    private bool turnsSet = false;

    private int index;

	// Use this for initialization
	public void StartTurnOrder () {
        index = 0;

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
        if (turnsSet)
        {
            return;
        }
        StartTurnOrder();
        turnsSet = true;
    }
	
    void SetTurns()
    {
        if (turnsSet)
            return;

        turnOrder = combatants;    
        turnOrder.Sort((p1, p2) => p1.GetComponent<Stats>().init.CompareTo(p2.GetComponent<Stats>().init));
        turnOrder.Reverse();

        turnsSet = true;
        DoTurns();
    }


    void DoTurns()
    {
        if (index > turnOrder.Count - 1)
        {
            index = 0;
        }
        if (turnOrder[index])
        {
            turnOrder[index].GetComponent<Turn>().myTurn = true;
        }
        else
        {
            index++;
            DoTurns();
        }

    }

    public void NextTurn()
    {
        turnOrder[index].GetComponent<Turn>().myTurn = false;
        index++;
        DoTurns();
    }

}
