using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public int pAction = 0;
    public int mAction = 0;
    public LayerMask mask;


    public GameObject[] enemies;

    public Color originalColor;

    private Stats player;
    private Turn thisTurn;

    void Start()
    {
        player = GetComponent<Stats>();
        thisTurn = GetComponent<Turn>();
        pAction = 0;
        mAction = 0;
    }

    public void FindEnemyToAttack () {

        if (!thisTurn.myTurn)
        {
            return;
        }

        if (pAction >= 1)
        {
            return;
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f, mask);

        for (int i = 0; i < hits.Length; i++)
        {
            for (int ii = 0; ii < enemies.Length; ii++)
            {
                if (enemies[ii] == hits[i].gameObject)
                {
                    originalColor = hits[i].gameObject.GetComponentInChildren< SpriteRenderer>().material.color;
                    hits[i].gameObject.GetComponentInChildren<SpriteRenderer>().material.color = Color.red;
                }
            }
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }



    public void Attack(GameObject enemy)
    {
        if (!thisTurn.myTurn)
        {
            return;
        }

        SpendPAction();
        enemy.GetComponent<Stats>().currentHP -= player.DMG;
        enemy.GetComponent<Turn>().CheckForDeath();

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponentInChildren<SpriteRenderer>().material.color = originalColor;
            }
        }

    }

    public void Heal()
    {
        if (!thisTurn.myTurn)
        {
            return;
        }
        if (mAction == 1)
        {
            if (pAction == 1)
            {
                return;
            }
            SpendPAction();
        }
        SpendMAction();

        player.currentHP += player.WISmod;
        if (player.currentHP >= player.maxHP)
        {
            player.currentHP = player.maxHP;
            return;
        }
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
