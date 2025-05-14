using TMPro;
using UnityEngine;
public class PaCat : MonoBehaviour
{
    enum CatState
    {
        idle, dash, jump, wallGrab, fall, climb, walk, run, crunch
    }

    public enum Direction
    {
        Left, Right
    }

    [SerializeField] LineRenderer line;
    [SerializeField] float force = 100;
    [SerializeField] float maxForceVectorLength = 4;
    Vector3 gameStartPos;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 startScale;
    float startGravity;
    CatState catState;
    

    bool addForce;
    Vector3 addForceVector;


    float totalClimbingTime = 3;
    float actClimbingTime = 0;
    Direction climbDirection;
    Vector2 climbPos;

    Rigidbody2D rb;
    SpriteRenderer sr;

    Animator animator;

    private void Start()
    {
        catState = CatState.idle;
        gameStartPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        startGravity = rb.gravityScale;
        startScale = transform.localScale;
        line.enabled = false;

        animator = GetComponent<Animator>();
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
        if (forceVector.magnitude > maxForceVectorLength)
        {
            forceVector.Normalize();
            forceVector *= maxForceVectorLength;
        }
        rb.gravityScale = startGravity;
        addForce = true;
        addForceVector = forceVector * force;

        //if (catState == CatState.idle)
        //{
        //    if (forceVector.y < -1)
        //    {
        //        catState = CatState.crunch;
        //        transform.localScale = new Vector3(startScale.x, startScale.y / 2, startScale.z);
        //    }
        //    else
        //    {
        //        rb.AddForce(forceVector * force, ForceMode2D.Force);
        //    }
        //}
        //else if (catState == CatState.crunch)
        //{
        //    if (forceVector.y > 0.2)
        //    {
        //        catState = CatState.idle;
        //        transform.localScale = startScale;
        //    }
        //    else
        //    {
        //        rb.AddForce(forceVector * force, ForceMode2D.Force);
        //    }
        //}
        //else
        //{

        //    rb.AddForce(forceVector * force, ForceMode2D.Force);
        //    catState = CatState.idle;
        //}

    }


    private void Update()
    {

        switch (catState)
        {
            case CatState.idle or CatState.wallGrab:
                
                animator.SetBool("Jump", false);
                animator.SetBool("Fall", false);
                
                if (addForce)
                {
                    addForce = false;

                    animator.SetBool("WallGrab", false);

                    //Flip sprite
                    if (addForceVector.x >= 0)
                        sr.flipX = false;
                    else
                        sr.flipX = true;



                    if (addForceVector.y < -2)
                    {
                        catState = CatState.crunch;
                        break;
                    }
                    else
                    {
                        rb.AddForce(addForceVector, ForceMode2D.Force);
                        addForce = false;
                    }
                }


                if (rb.linearVelocity.y > 2)
                    catState = CatState.jump;
                else if (rb.linearVelocity.y < -0.5)
                    catState = CatState.fall;
                else if (Mathf.Abs(rb.linearVelocity.x) > 0.5)
                    catState = CatState.walk;

                break;
            case CatState.dash:
                break;
            case CatState.jump:
                animator.SetBool("Jump", true);
                if (rb.linearVelocity.y < -0.5)
                {
                    catState = CatState.fall;
                    
                }
                break;

            case CatState.fall:
                animator.SetBool("WallGrab", false);
                animator.SetBool("Jump", false);
                animator.SetBool("Fall", true);
                if (rb.linearVelocity == Vector2.zero)
                {
                    catState = CatState.idle;
                    animator.SetBool("Fall", false);
                }



                break;
            case CatState.climb:
                actClimbingTime += Time.deltaTime;
                if (actClimbingTime >= totalClimbingTime)
                {
                    catState = CatState.idle;
                    actClimbingTime = 0;
                    if (climbDirection == Direction.Left)
                    {
                        transform.position = new Vector3(climbPos.x + transform.localScale.x / 2, climbPos.y + transform.localScale.y / 2);
                    }
                    rb.gravityScale = startGravity;
                }
                break;
            case CatState.walk:
                if (Mathf.Abs(rb.linearVelocity.x) > 1)
                    catState = CatState.run;
                else if (Mathf.Abs(rb.linearVelocity.x) < 0.1)
                    catState = CatState.idle;
                break;
            case CatState.run:
                if (Mathf.Abs(rb.linearVelocity.x) < 0.5)
                    catState = CatState.walk;
                break;
            case CatState.crunch:
                transform.localScale = new Vector3(startScale.x, startScale.y / 2, startScale.z);
                if (addForce)
                {
                    addForce = false;
                    if (addForceVector.y > 2)
                    {
                        catState = CatState.idle;
                        transform.localScale = startScale;
                        break;
                    }
                }
                break;
            default:
                break;
        }

        try
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = catState.ToString();
        }
        catch (System.Exception)
        {


        }




        //if (catState != CatState.climb && catState != CatState.hang && catState != CatState.crunch)
        //{
        //    if (rb.linearVelocity != Vector2.zero)
        //    {
        //        if (rb.linearVelocity.y > 1 && rb.linearVelocity.y <= 2)
        //            catState = CatState.dash;
        //        else if (rb.linearVelocity.y > 2)
        //            catState = CatState.jump;
        //        else if (rb.linearVelocity.y < -0.5)
        //            catState = CatState.fall;
        //        else if (Mathf.Abs(rb.linearVelocity.x) > 1 && Mathf.Abs(rb.linearVelocity.x) <= 4)
        //            catState = CatState.walk;
        //        else if (Mathf.Abs(rb.linearVelocity.x) > 4)
        //            catState = CatState.run;
        //        else
        //            catState = CatState.idle;
        //        //Debug.Log(rd.linearVelocity);
        //    }
        //    else
        //    {
        //        catState = CatState.idle;
        //    }
        //}






        if (line.enabled == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            endPos = ray.origin;
            line.SetPosition(0, new Vector3(endPos.x, endPos.y, 0));
            line.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 0));
            float lineLength = Vector3.Distance(line.GetPosition(0), line.GetPosition(1));
            line.startColor = Color.Lerp(Color.green, Color.red, lineLength / maxForceVectorLength);
            line.endColor = line.startColor;
            //Debug.Log(lineLength);
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
        PaHangable ph = collision.transform.GetComponent<PaHangable>();

        if (ph != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = ph.gravityModifier;
            catState = CatState.wallGrab;
            animator.SetBool("WallGrab", true);
            Debug.Log("ColEnter" + ph.name);
        }



        ////Debug.Log(collision.GetContact(0).point);

        //if (collision.GetContact(0).point.y > transform.position.y) //Head collide
        //{
        //    Debug.Log("Head col");
        //    catState = CatState.crunch;
        //    transform.localScale = new Vector3(startScale.x, startScale.y / 2, startScale.z);
        //    rb.linearVelocity = Vector2.zero;
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //rd.gravityScale = startGravity;
        //catState = CatState.jump;
        //Debug.Log("ColExit");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PaClimbable pc = collision.transform.GetComponent<PaClimbable>();

        if (pc != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
            climbDirection = pc.climbDirection;
            climbPos = pc.transform.position;
            catState = CatState.climb;
            Debug.Log("TrgEnter" + pc.name);
        }


    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //rb.gravityScale = startGravity;
        //catState = CatState.walk;
        //Debug.Log("TrgExit");
    }

}
