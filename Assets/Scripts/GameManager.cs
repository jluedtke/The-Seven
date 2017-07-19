using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public bool playerTurn = false;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update () {
		
	}
}
