using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedMovement2D : MonoBehaviour
{
    public bool tieSpeedToBall;
    public float xSpeed;
    public float ySpeed;
    public Vector2 maxSpeed;
    /// <summary>
    /// X and Y values that define the range of movement that is allowed.
    /// if both are left as zero, this will go unused.
    /// ex: if movement limit is (5, 0), and the starting point is (0, 0) then X can go from (-2.5,0) to (2.5,0) 
    /// </summary>
    public Vector2 movementLimit;
    private Vector2 _startPos;
    private float timer = 0.0f;
    private float waitTime = 0.2f;
    private SuperBall ball;

    // Start is called before the first frame update
    void Start()
    {
        if (tieSpeedToBall)
        {
            ball = GetComponentInChildren<SuperBall>();
            SyncSpeedToBall();
        }
        _startPos = transform.localPosition;
    }

    void Update()
    {
        if (tieSpeedToBall)
        {
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                SyncSpeedToBall();
                timer -= waitTime;
            }
        }
    }

    void FixedUpdate()
    {
        if (!movementLimit.Equals(Vector2.zero)){
            LimitMovement();
        }

        transform.Translate(new Vector3(xSpeed, ySpeed, 0));

    }

    private void LimitMovement()
    {
        Vector2 currPos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        Vector2 distance = _startPos - currPos;
        if(distance.x > movementLimit.x/2.0f || distance.x < -movementLimit.x / 2.0f)
        {
            xSpeed *= -1;
        }
        if (distance.y > movementLimit.y / 2.0f || distance.y < -movementLimit.y / 2.0f)
        {
            ySpeed *= -1;
        }
    }

    private void SyncSpeedToBall()
    {
        if (xSpeed < maxSpeed.x)
        {
            this.xSpeed = Mathf.Abs(ball.Speed.x);
        }
        if (ySpeed < maxSpeed.y)
        {
            Debug.Log(ySpeed);
            this.ySpeed = Mathf.Abs(ball.Speed.y);
        }
    }
}
