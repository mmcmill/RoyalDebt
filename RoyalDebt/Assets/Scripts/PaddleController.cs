using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleController : MonoBehaviour
{
    private float _movementSpeed = 0.5f;
    public int score = 0;
    public int scoreInc = 5;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
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
            this.score += scoreInc;
            Debug.Log(score);
        }
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainScene")
        {
            Destroy(gameObject);
            Debug.Log("Resetting Paddle");
        }
    }
}
