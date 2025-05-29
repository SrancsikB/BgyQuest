using UnityEngine;

public class PaMouse : MonoBehaviour
{

    [SerializeField] float force=3;

    public float direction = 1f;
    public Vector3 endPos;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity=Vector2.right * force*direction;
        if (direction>0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (transform.position.x>endPos.x)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (transform.position.x < endPos.x)
            {
                Destroy(this.gameObject);
            }
        }
        //rb.AddForce(Vector2.right * force, ForceMode2D.Force);
    }
}
