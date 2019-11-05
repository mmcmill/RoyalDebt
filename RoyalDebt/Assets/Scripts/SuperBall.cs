using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuperBall : MonoBehaviour
{
    public Vector2 minSpeed;
    private Vector2 speed;
    public Vector2 maxSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        speed = new Vector2(Random.value*maxSpeed.x, maxSpeed.y);
        Debug.Log(speed);
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
