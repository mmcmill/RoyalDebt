using System.Collections;
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

    [TextArea]
    public List<string> attackDialogueOptions;
    [TextArea]
    public List<string> deathDialogueOptions;
    [TextArea]
    public List<string> bribeDialogueOptions;


    public AudioClip ballHit;
    public AudioClip bribeHit;
    public AudioClip ballDeath;
    public AudioClip bribeDeath;

    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > deltaTDamage)
        {
            var pc = GameObject.FindObjectOfType<PaddleController>();
            string dialogue = attackDialogueOptions[Random.Range(0, attackDialogueOptions.Count)];
            DisplayDialogue(dialogue);
            pc.PublicOpinion -= damageToPubOpin;
            timer -= deltaTDamage;
        }
    }

    private void DisplayDialogue(string dialogue)
    {
        Debug.Log(dialogue);
        //TODO dialogue system
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = ballHit;
            audioSource.Play();
            this.health -= damageTaken;
            if(this.health < 0)
            {
                AudioSource.PlayClipAtPoint(ballDeath, transform.position);
                string dialogue = deathDialogueOptions[Random.Range(0, deathDialogueOptions.Count)];
                DisplayDialogue(dialogue);
                Destroy(gameObject);
            }
        } if(collision.gameObject.tag == "Projectile") {
            Projectile hitBy = collision.gameObject.GetComponent<Projectile>();
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = bribeHit;
           
            moneyToBribe -= hitBy.damage;
            if (this.moneyToBribe < 0)
            {
                AudioSource.PlayClipAtPoint(bribeDeath, transform.position);
                string dialogue = bribeDialogueOptions[Random.Range(0, bribeDialogueOptions.Count)];
                DisplayDialogue(dialogue);
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
