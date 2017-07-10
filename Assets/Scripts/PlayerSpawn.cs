using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    public GameObject playerPrefab;

    private Transform waypoint;

    // Use this for initialization
    void Start () {

        if (playerPrefab != null)
        {
            waypoint = transform.GetChild(1);
            Instantiate(playerPrefab, waypoint.position, Quaternion.identity, transform);

        }
    }
}
