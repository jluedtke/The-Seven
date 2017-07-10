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

    public void ChangeSprite(Vector3 dir)
    {
        if (dir.y > 0)
        {
            sr.sprite = forwardSprite;
            sr.flipX = false;
        }
        if (dir.y < 0)
        {
            sr.sprite = backwardSprite;
            sr.flipX = false;
        }
        if (dir.x > 0)
        {
            sr.sprite = sidewaysSprite;
            sr.flipX = false;
        }
        if (dir.x < 0)
        {
            sr.sprite = sidewaysSprite;
            sr.flipX = true;
        }

    }
}
