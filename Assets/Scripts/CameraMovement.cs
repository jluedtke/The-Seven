using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (!float.IsNaN(transform.position.x))
        {
            transform.position = player.transform.position + offset;
        }
        return;
    }
}

