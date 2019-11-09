using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleController : MonoBehaviour
{
    private float _movementSpeed = 0.5f;
    private float _stockPrice = 0;
    public float StockPrice { get => _stockPrice;
        set
        {
            _stockPrice = value;
            ScoreDisplay.UpdateScoreDisplay(this);
        }
    }
    private int _stockInc;
    private int _funds = 0;
    public int Funds { get => _funds;
        set
        {
            _funds = value;
            ScoreDisplay.UpdateFundsDisplay(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
