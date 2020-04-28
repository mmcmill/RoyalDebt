using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject owner;
    public Vector2 speed;
    private Vector2 _startPos;
    public float maxRange;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(_startPos, transform.localPosition) >= maxRange)
        {
            Destroy(gameObject);
        }

        transform.Translate(new Vector3(speed.x, speed.y, 0));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject != owner)
        {
            Destroy(gameObject);
        }
    }
}
