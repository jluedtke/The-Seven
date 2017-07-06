using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public bool playerTurn = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
    }

    public Vector2 mapSize;
    public Vector3 tileSize;
    public Vector2 offset;

    GameObject[] tileSet;
    GameObject currentTile;
    int index;

    // Map generation. Should be in another script.
    void Start()
    {
        tileSet = GameObject.FindGameObjectsWithTag("Finish");
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                index = Random.Range(0, tileSet.Length);
                currentTile = tileSet[index];
                GameObject obj = (GameObject)Instantiate(currentTile);
                obj.transform.position = new Vector3(x * tileSize.x + offset.x, y * tileSize.y + offset.y, -.1f);
            }
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}

// TODO: change movement to once per turn
//       might have to refactor movement

// TODO: put movement in a single script and call from there
