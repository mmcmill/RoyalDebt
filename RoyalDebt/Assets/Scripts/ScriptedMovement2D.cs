using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedMovement2D : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(xSpeed, ySpeed, 0));
        Debug.Log(transform);
    }
}
