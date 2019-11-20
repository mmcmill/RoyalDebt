﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadEnemy : MonoBehaviour
{
    // health pool
    public float health;
    // how much health is taken per ball hit
    public float damageTaken;
    //how much money it takes to bribe head enemy.
    public float moneyToBribe;
    // how much damage to do to public opinion of player
    public float damageToPubOpin;
    // How often to inflict damage to public opinion of player
    public float deltaTDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            this.health -= damageTaken;
            if(this.health < 0)
            {
                Destroy(gameObject);
            }
        } else
        {
            //Get the first contact point's normal
            Vector2 normal = collision.contacts[0].normal;
            ScriptedMovement2D mov = GetComponent<ScriptedMovement2D>();

            if (normal.x != 0)
            {
                mov.xSpeed *= -1;
            }
            if (normal.y != 0)
            {
                mov.ySpeed *= -1;
            }
        }
    }
}
