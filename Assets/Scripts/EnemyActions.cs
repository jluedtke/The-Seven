using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour {

    public int pAction = 0;
    public int mAction = 0;
    public LayerMask mask;


    private Stats playerStats;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<Stats>();
    }

    public void AttackPlayer()
    {
        if (pAction > 0)
        {
            return;
        }
        SpendPAction();
        playerStats.currentHP -= GetComponent<Stats>().DMG;
    }

    void SpendMAction()
    {
        if (mAction == 1)
        {
            return;
        }
        mAction++;
    }

    void SpendPAction()
    {
        if (pAction == 1)
        {
            return;
        }
        pAction++;
    }
}
