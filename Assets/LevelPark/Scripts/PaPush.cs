using TMPro;
using UnityEngine;
public class PaPush : MonoBehaviour
{
    enum CatState
    {
        idle, dash, jump, hang, fall, climb, walk, run, crunch
    }


    [SerializeField] LineRenderer line;
    [SerializeField] float force = 1;
    [SerializeField] float hangGravityModifier;
    Vector3 gameStartPos;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 startScale;
    float startGravity;
    CatState catState;

    Rigidbody2D rb;

    private void Start()
    {
        catState = CatState.idle;
        gameStartPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        startGravity = rb.gravityScale;
        startScale = transform.localScale;
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

        rb.gravityScale = startGravity;
        if (catState == CatState.idle)
        {
            if (forceVector.y < -1)
            {
                catState = CatState.crunch;
                transform.localScale = new Vector3(startScale.x, startScale.y / 2, startScale.z);
            }
            else
            {
                rb.AddForce(forceVector * force, ForceMode2D.Force);
            }
        }
        else if (catState == CatState.crunch)
        {
            if (forceVector.y > 0.2)
            {
                catState = CatState.idle;
                transform.localScale = startScale;
            }
            else
            {
                rb.AddForce(forceVector * force, ForceMode2D.Force);
            }
        }
        else
        {

            rb.AddForce(forceVector * force, ForceMode2D.Force);
            catState = CatState.idle;
        }

    }


    private void Update()
    {
        if (catState != CatState.climb && catState != CatState.hang && catState != CatState.crunch)
        {
            if (rb.linearVelocity != Vector2.zero)
            {
                if (rb.linearVelocity.y > 1 && rb.linearVelocity.y <= 2)
                    catState = CatState.dash;
                else if (rb.linearVelocity.y > 2)
                    catState = CatState.jump;
                else if (rb.linearVelocity.y < -0.5)
                    catState = CatState.fall;
                else if (Mathf.Abs(rb.linearVelocity.x) > 1 && Mathf.Abs(rb.linearVelocity.x) <= 4)
                    catState = CatState.walk;
                else if (Mathf.Abs(rb.linearVelocity.x) > 4)
                    catState = CatState.run;
                else
                    catState = CatState.idle;
                //Debug.Log(rd.linearVelocity);
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
            rb.gravityScale = startGravity;
            rb.linearVelocity = Vector2.zero;
            transform.position = gameStartPos;

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Hang
        PaHangable ph = collision.transform.GetComponent<PaHangable>();

        if (ph != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = hangGravityModifier;
            catState = CatState.hang;
            Debug.Log("ColEnter" + ph.name);
        }



        //Debug.Log(collision.GetContact(0).point);

        if (collision.GetContact(0).point.y>transform.position.y) //Head collide
        {
            Debug.Log("Head col");
            catState = CatState.crunch;
            transform.localScale = new Vector3(startScale.x, startScale.y / 2, startScale.z);
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.gravityScale = startGravity;
        //catState = CatState.jump;
        Debug.Log("ColExit");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PaClimbable pc = collision.transform.GetComponent<PaClimbable>();

        if (pc != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
            catState = CatState.climb;
            Debug.Log("TrgEnter" + pc.name);
        }


    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        rb.gravityScale = startGravity;
        catState = CatState.walk;
        Debug.Log("TrgExit");
    }


}
