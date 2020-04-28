using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleController : MonoBehaviour
{
    private float _movementSpeed = 0.7f;
    private float _stockPrice;
    public float StockPrice { get => _stockPrice;
        set
        {
            _stockPrice = value;
            ScoreDisplay.UpdateScoreDisplay(this);
        }
    }
    private int _stockInc;
    private int _funds= 1000;
    public int Funds { get => _funds;
        set
        {
            _funds = value;
            ScoreDisplay.UpdateFundsDisplay(this);
            if (_funds <= 0)
            {
                _funds = 0;
            }
        }
    }
    public int fundsPerProjectile;

    public HealthBar pubOpinionBar;
    private readonly static int PUBLIC_OPINION_MAX = 100;
    private readonly static int PUBLIC_OPINION_MIN = 0;
    private int _publicOpinion = PUBLIC_OPINION_MAX;
    public int PublicOpinion { get => _publicOpinion;
        set
        {
            if (_publicOpinion <= PUBLIC_OPINION_MIN)
            {
                _publicOpinion = PUBLIC_OPINION_MIN;
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                _publicOpinion = value;
                Debug.Log(_publicOpinion);
                if (pubOpinionBar != null)
                {
                    pubOpinionBar.UpdateBar(_publicOpinion, PUBLIC_OPINION_MAX);
                }
                if (_publicOpinion > PUBLIC_OPINION_MAX) _publicOpinion = PUBLIC_OPINION_MAX;
            }
        }
    }

    public Projectile projectile;

    private float timer = 0.0f;
    public float fireRate;

    void Update()
    {
        timer += Time.deltaTime;

        float posY = transform.position.y * 10;
        if(posY > StockPrice)
        {
            StockPrice = posY;
        }

        if (Input.GetKey(KeyCode.Space) && timer > fireRate && projectile != null)
        {
            ShootFunds();
            timer = 0f;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector3.left * _movementSpeed);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(Vector3.right * _movementSpeed);
        }
    }

    private void ShootFunds()
    {
        // can only shoot if enough funds are available
        if (Funds >= fundsPerProjectile)
        {
            Funds -= fundsPerProjectile;

            Projectile p = Instantiate<Projectile>(projectile, transform.position, transform.rotation, GetComponentInParent<Camera>().transform);
            p.owner = gameObject;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Projectile") return;

        // otherwise we prevent the paddle from moving in that direction
        Vector2 normal = collision.GetContact(0).normal;
        this.transform.Translate( new Vector3(normal.x*_movementSpeed, 0));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            this.Funds += 500;//STIMULUS
            GetComponent<AudioSource>().Play();
        }
    }
}
