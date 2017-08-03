using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromPlayerToEnemy : MonoBehaviour {

    //private Color originalColor;
    private SpriteRenderer spriteColor;
    private PlayerActions playerAttacking;
    private Stats pStats;

    private void Start()
    {
        spriteColor = GetComponentInChildren<SpriteRenderer>();
        //originalColor = spriteColor.material.color;
        playerAttacking = GameObject.Find("Player").GetComponent<PlayerActions>();
        pStats = GameObject.Find("Player").GetComponent<Stats>();

    }

    void OnMouseEnter()
    {
        if (spriteColor.material.color == Color.red)
        {
            spriteColor.material.color = Color.yellow;
        }
    }

    void OnMouseExit()
    {
        if (spriteColor.material.color == Color.yellow)
        {
            spriteColor.material.color = Color.red;
        }
    }

    private void OnMouseDown()
    {
        if (playerAttacking.pAction > 0 || (spriteColor.material.color != Color.yellow)) // If player has dbl healed or attacked already or enemy outside of range
        {
            return;
        }
        playerAttacking.Attack(this.gameObject);
    }

}
