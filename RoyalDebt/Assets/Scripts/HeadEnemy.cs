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

    public int pubOpinBack;

    private AudioSource audioSource;
    public AudioClip ballHit;
    public AudioClip bribeHit;
    public AudioClip ballDeath;
    public AudioClip bribeDeath;

    private float timer = 0.0f;
    private Animator animator;

    private ScriptedMovement2D mov;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        mov = GetComponent<ScriptedMovement2D>();
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
        if (collision.gameObject.tag == "Player") return;

        if (collision.gameObject.tag == "Ball")
        {
            if (animator != null) animator.SetTrigger("IsLegalHit");
            this.health -= damageTaken;
            audioSource.PlayOneShot(ballHit);

            if (this.health <= 0)
            {
                if(animator != null) animator.SetBool("IsLegalDeath", true);
                audioSource.PlayOneShot(ballDeath);
                if (animator == null) Destroy(gameObject); // we need to destroy the game object, instead of the death animation
                else Destroy(this);
            }

        }
        if (collision.gameObject.tag == "Projectile")
        {
            Projectile hitBy = collision.gameObject.GetComponent<Projectile>();
            if (animator != null) animator.SetTrigger("IsBribeHit");
            audioSource.PlayOneShot(bribeHit);
            moneyToBribe -= hitBy.damage;
            if (this.moneyToBribe <= 0)
            {
                if(animator!=null) animator.SetBool("IsBribeDeath", true);
                audioSource.PlayOneShot(bribeDeath);
                hitBy.owner.GetComponent<PaddleController>().PublicOpinion += pubOpinBack;
                if (animator == null) Destroy(gameObject); // we need to destroy the game object, instead of the death animation
                else Destroy(this);
            }
        }
        else
        {
            //Get the first contact point's normal
            Vector2 normal = collision.contacts[0].normal;

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
