using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (!float.IsNaN(transform.position.x))
        {
            transform.position = player.position + offset;
        }
    }
}

