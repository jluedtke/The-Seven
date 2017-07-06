using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject {

    // Player info like HP, DMG, ect goes here
    // might only activate this during combat movement



    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }


    // Update is called once per frame
    void Update()
    {
        // call if(!GameManager.instance.playersTurn) return true; to check for player turn

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        //if (horizontal != 0)
        //{
        //    vertical = 0;
        //}

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Enemy>(horizontal, vertical);  //Change to <enemy> 
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        // Everytime player moves subtract Time-- or something

        base.AttemptMove<T>(xDir, yDir);
        RaycastHit2D hit;

        if (Move(xDir, yDir, out hit))
        {

            // When hitting an enemy, sound file instagator/animation
        }
    }

    protected override void OnCantMove<T>(T componant)
    {
        // write -- Enemy hitEnemy = componant as Enemy;
        // write -- hitEnemy.damageEnemy(damage); -- where damage is var in Player.
        // write -- animator.SetTrigger("playerSwing");
    }

    public void takeDamage (int loss)
    {
        // animator.SetTrigger("playerHit");
        // health -= loss;
        // CheckIfGameOver();
    }

    public void CheckIfGameOver()
    {
        //if (health <= 0)
        //{
        //    GameManager.intance.GameOver();
        //}
    }
}
