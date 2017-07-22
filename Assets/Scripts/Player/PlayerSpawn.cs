using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    public GameObject Player;

    private Transform waypoint;

    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");
        if (Player != null)
        {
            waypoint = transform.GetChild(1);
            Player.transform.position = waypoint.transform.position;
        }
    }
}
