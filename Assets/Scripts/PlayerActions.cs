using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public int pAction = 0;
    public int mAction = 0;
    public LayerMask mask;


    public List<GameObject> enemies;

    public Color originalColor;

    private Stats player;
    private Turn thisTurn;

    void Start()
    {
        player = GetComponent<Stats>();
        thisTurn = GetComponent<Turn>();
        pAction = 0;
        mAction = 0;
        InvokeRepeating("DefineEnemies", 1f, 1f);
    }

    public List<GameObject> DefineEnemies()
    {
        enemies = new List<GameObject>();
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, 8f, mask);
        if (enemyColliders.Length > 0)
        {
            for (int x = 0; x < enemyColliders.Length; x++)
            {
                enemies.Add(enemyColliders[x].gameObject);
            }
        }

        return enemies;
    }

    public void FindEnemyToAttack () {

        DefineEnemies();

        if (!thisTurn.myTurn)
        {
            return;
        }

        if (pAction >= 1)
        {
            return;
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f, mask);

        for (int i = 0; i < hits.Length; i++)
        {
            for (int ii = 0; ii < enemies.Count; ii++)
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
        Gizmos.DrawWireSphere(transform.position, 8f);

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

        for (int i = 0; i < enemies.Count; i++)
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
