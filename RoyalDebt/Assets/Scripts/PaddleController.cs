using System.Collections;
using System.Collections.Generic;
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
    private int _funds;
    public int Funds { get => _funds;
        set
        {
            _funds = value;
            ScoreDisplay.UpdateFundsDisplay(this);
            if (_funds <= 0)
            {
                new WaitForSeconds(2);
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public HealthBar pubOpinionBar;
    private readonly static int PUBLIC_OPINION_MAX = 100;
    private readonly static int PUBLIC_OPINION_MIN = 0;
    private int _publicOpinion;
    public int PublicOpinion { get => _publicOpinion;
        set
        {
            _publicOpinion = value;
            Debug.Log(_publicOpinion);
            if (pubOpinionBar != null)
            {
                pubOpinionBar.UpdateBar(_publicOpinion, PUBLIC_OPINION_MAX);
            }
            if(_publicOpinion <= PUBLIC_OPINION_MIN)
            {
                _publicOpinion = PUBLIC_OPINION_MIN;
                new WaitForSeconds(2);
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StockPrice = 0;
        Funds = 1000;
        PublicOpinion = PUBLIC_OPINION_MAX;
       // DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        float posY = transform.position.y * 10;
        if(posY > StockPrice)
        {
            StockPrice = posY;
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

        if (Input.GetKey(KeyCode.Space))
        {
            ShootFunds();
        }
    }

    private void ShootFunds()
    {
        throw new System.NotImplementedException();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball") return;

        // otherwise we prevent the paddle from moving in that direction
        Vector2 normal = collision.GetContact(0).normal;
        this.transform.Translate( new Vector3(normal.x*_movementSpeed, 0));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            this.Funds += (int)(StockPrice/4);//STIMULUS
            GetComponent<AudioSource>().Play();
        }
    }


    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if(scene.name == "MainScene")
    //    {
    //        Destroy(gameObject);
    //        Debug.Log("Resetting Paddle");
    //    }
    //}
}
