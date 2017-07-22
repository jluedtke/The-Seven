using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public int pAction = 0;
    public int mAction = 0;
    public LayerMask mask;


    private GameObject[] enemies;

    private Color originalColor;

    private Stats player;

    void Start()
    {
        player = GetComponent<Stats>();
        pAction = 0;
        mAction = 0;
    }

    void OnMouseDown()
    {
        Debug.Log("Hovering");
        if (gameObject.GetComponentInChildren<SpriteRenderer>().material.color == Color.red)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().material.color = Color.yellow;
        }
    }

    void OnMouseExit()
    {
        if (gameObject.GetComponentInChildren<SpriteRenderer>().material.color == Color.yellow)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().material.color = originalColor;
        }
    }

    public void FindEnemyToAttack () {
        //if (pAction >= 1)
        //{
        //    return;
        //}
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f, mask);

        for (int i = 0; i < hits.Length; i++)
        {
            for (int ii = 0; ii < enemies.Length; ii++)
            {
                if (enemies[ii] == hits[i].gameObject)
                {
                    Debug.Log("Enemy Hit");
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



    //void AttackEnemy(GameObject enemy)
    //{
    //    if (Contains(enemies, enemy))
    //    {
    //        enemy.GetComponent<Stats>().currentHP -= player.DMG;
    //        for (int ii = 0; ii < enemies.Length; ii++)
    //        {
    //            enemies[ii].GetComponent<Renderer>().material.color = originalColor;
    //        }
    //        SpendPAction();
    //    }
    //}

    public void Heal()
    {
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
