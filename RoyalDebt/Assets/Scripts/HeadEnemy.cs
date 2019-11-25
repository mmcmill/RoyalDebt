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
    public int damageToPubOpin;
    // How often to inflict damage to public opinion of player
    public float deltaTDamage;

    public AudioClip ballHit;
    public AudioClip bribeHit;
    public AudioClip ballDeath;
    public AudioClip bribeDeath;

    private float timer = 0.0f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > deltaTDamage && health > 0 && moneyToBribe > 0)
        {
            var pc = GameObject.FindObjectOfType<PaddleController>();
            pc.PublicOpinion -= damageToPubOpin;
            timer -= deltaTDamage;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            AudioSource audioSource = GetComponent<AudioSource>();

            this.health -= damageTaken;
            if (this.health < 0)
            {
                if(animator != null) animator.SetBool("IsLegalDeath", true);
                AudioSource.PlayClipAtPoint(ballDeath, transform.parent.position, .2f);
                Destroy(gameObject);
            }

            audioSource.PlayOneShot(ballHit);
        }
        if (collision.gameObject.tag == "Projectile")
        {
            Projectile hitBy = collision.gameObject.GetComponent<Projectile>();
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(bribeHit);
            moneyToBribe -= hitBy.damage;
            if (this.moneyToBribe < 0)
            {
                if(animator!=null) animator.SetBool("IsBribeDeath", true);
                AudioSource.PlayClipAtPoint(bribeDeath, transform.parent.position, .2f);
                hitBy.owner.GetComponent<PaddleController>().PublicOpinion += 10;
                Destroy(gameObject);
            }
        }
        else
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
