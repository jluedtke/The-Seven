using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour {

    public int pAction = 0;
    public int mAction = 0;
    public LayerMask mask;


    private List<GameObject> enemies;

    void Start()
    {
        InvokeRepeating("FindEnemyToAttack", 0, .5f);
    }

	void FindEnemyToAttack () {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, .8f, mask);

        for (int i = 0; i < hits.Length; i++)
        {
            enemies.Add(hits[i].gameObject);
            Debug.Log(hits[i].gameObject);
        }
    }
}
