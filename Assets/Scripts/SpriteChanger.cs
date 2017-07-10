using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour {

    public Sprite forwardSprite;
    public Sprite backwardSprite;
    public Sprite sidewaysSprite;

    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = backwardSprite;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            sr.sprite = forwardSprite;
            sr.flipX = false;
        }
        if (Input.GetKey("s"))
        {
            sr.sprite = backwardSprite;
            sr.flipX = false;
        }
        if (Input.GetKey("d"))
        {
            sr.sprite = sidewaysSprite;
            sr.flipX = false;
        }
        if (Input.GetKey("a"))
        {
            sr.sprite = sidewaysSprite;
            sr.flipX = true;
        }

    }
}
