using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject enemyPrefab;
    private Transform waypoint;

    // Use this for initialization
    void Start()
    {

        if (enemyPrefab != null)
        {
            waypoint = transform.GetChild(1);
            Instantiate(enemyPrefab, waypoint.position, Quaternion.identity, transform);
        }
    }
}
