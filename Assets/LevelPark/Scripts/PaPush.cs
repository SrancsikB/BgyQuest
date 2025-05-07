using UnityEngine;
using TMPro;
public class PaPush : MonoBehaviour
{
    enum CatState
    {
        idle, dash, jump, climb, fall
    }


    [SerializeField] LineRenderer line;
    [SerializeField] float force = 1;
    Vector3 gameStartPos;
    Vector3 startPos;
    Vector3 endPos;
    float startGravity;
    CatState catState;

    Rigidbody2D rd;

    private void Start()
    {
        catState = CatState.idle;
        gameStartPos = transform.position;
        rd = GetComponent<Rigidbody2D>();
        startGravity = rd.gravityScale;
        line.enabled = false;
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit;
        hit = Physics2D.GetRayIntersection(ray);
        startPos = hit.transform.position;

        line.SetPosition(0, startPos);
        line.SetPosition(1, startPos);
        line.enabled = true;

    }

    private void OnMouseUp()
    {
        line.enabled = false;

        Vector2 forceVector = new Vector2(startPos.x - endPos.x, startPos.y - endPos.y);
        rd.AddForce(forceVector * force, ForceMode2D.Impulse);
        rd.gravityScale = startGravity;

    }


    private void Update()
    {
        if (catState != CatState.climb)
        {
            if (rd.linearVelocity != Vector2.zero)
            {
                if (rd.linearVelocity.y > 1 && rd.linearVelocity.y <= 2)
                    catState = CatState.dash;
                else if (rd.linearVelocity.y > 2)
                    catState = CatState.jump;
                else if (rd.linearVelocity.y < -0.5)
                    catState = CatState.fall;
                else
                    catState = CatState.idle;
                Debug.Log(rd.linearVelocity);
            }
            else
            {
                catState = CatState.idle;
            }
        }


        transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = catState.ToString();

        if (line.enabled == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            endPos = ray.origin;
            line.SetPosition(0, endPos);
            line.SetPosition(1, transform.position);
        }

        if (transform.position.y < -5)
        {
            rd.gravityScale = startGravity;
            rd.linearVelocity = Vector2.zero;
            transform.position = gameStartPos;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PaClimbable pc = collision.transform.GetComponent<PaClimbable>();

        if (pc != null)
        {
            rd.linearVelocity = Vector2.zero;
            rd.gravityScale = pc.gravityModifier;
            catState = CatState.climb;
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rd.gravityScale = startGravity;
        catState = CatState.jump;
    }

}
