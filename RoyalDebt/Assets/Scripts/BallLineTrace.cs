using UnityEngine;

public class BallLineTrace : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private bool drawSwitch; //just used so we aren't updating point every frame.

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(drawSwitch)
            lineRenderer.SetPosition(lineRenderer.positionCount-1, new Vector3(transform.position.x, transform.position.y, 0));
        drawSwitch = !drawSwitch;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // likely uses local space, need to make function to translate to world space.
        Vector2 vec = collision.contacts[0].point;
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, new Vector3(vec.x, vec.y, 0));
    }
}
