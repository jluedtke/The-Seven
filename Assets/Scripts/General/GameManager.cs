using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public PlayerActions pActions;
    public TurnManager battleManager;



    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pActions = GameObject.Find("Player").GetComponent<PlayerActions>();
        battleManager = GameObject.Find("BattleManager").GetComponent<TurnManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (pActions.enemies.Count <= 0)
        {
            battleManager.gameObject.SetActive(false);
        } else
        {
            battleManager.gameObject.SetActive(true);
        }
    }
}
