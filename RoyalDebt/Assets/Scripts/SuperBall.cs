using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuperBall : MonoBehaviour
{
    public Vector2 minSpeed;
    private Vector2 speed; 
    public Vector2 Speed => speed;
    public Vector2 maxSpeed;
    public Vector2 scaleSpeedPerSecond;
    private float timer = 0.0f;
    private float waitTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
        speed = new Vector2(Random.Range(0.1f, 1.0f) *minSpeed.x, minSpeed.y);
        Debug.Log(speed);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > waitTime)
        {
            if (Mathf.Abs(speed.x) < Mathf.Abs(maxSpeed.x))
            {
                speed.x *= scaleSpeedPerSecond.x;
            }
            if (Mathf.Abs(speed.y) < Mathf.Abs(maxSpeed.y))
            {
                speed.y *= scaleSpeedPerSecond.y;
            }
            timer -= waitTime;
        }
    }

    void FixedUpdate()
    {
        this.transform.Translate(speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Get the first contact point's normal
        Vector2 normal = collision.contacts[0].normal;
        
        if(normal.x != 0)
        {
            speed.x *= -1;
        }
        if(normal.y != 0)
        {
            speed.y *= -1;
        }

        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Finish"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
