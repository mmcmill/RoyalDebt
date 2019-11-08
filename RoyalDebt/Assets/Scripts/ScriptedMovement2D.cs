using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedMovement2D : MonoBehaviour
{
    public bool tieSpeedToBall;
    public float xSpeed;
    public float ySpeed;
    private float timer = 0.0f;
    private float waitTime = 0.2f;
    private SuperBall ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponentInChildren<SuperBall>();
        SyncSpeedToBall();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            SyncSpeedToBall();
            timer -= waitTime;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(xSpeed, ySpeed, 0));
    }

    void SyncSpeedToBall()
    {
        this.xSpeed = Mathf.Abs(ball.Speed.x);
        this.ySpeed = Mathf.Abs(ball.Speed.y);
    }
}
