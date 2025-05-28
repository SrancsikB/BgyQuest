using UnityEngine;
public class PaCat : MonoBehaviour
{
    enum CatState
    {
        idle, dash, jump, wallGrab, fall, ledgeGrab, ledgeStruggle, ledgeClimb, walk, run, crouch, sneak, sleep, fright, die
    }

    public enum Direction
    {
        Left, Right
    }

    [SerializeField] LineRenderer line;
    [SerializeField] float force = 100;
    [SerializeField] float hangGravityModifier = 0.0f;
    [SerializeField] float maxForceVectorLength = 4;
    [SerializeField] float startEnergy = 100;
    Vector3 gameStartPos;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 startScale;
    float startGravity;
    CatState catState;
    CatState catStateLast;
    float currentEnergy;

    bool addForce;
    Vector3 addForceVector;

    Direction climbDirection;
    Vector2 climbPos;

    bool keepCrouch;
    bool canSleep;

    Rigidbody2D rb;
    SpriteRenderer sr;
    BoxCollider2D boxCollider;
    Vector2 boxColliderOrigiSize;

    Animator animator;

    Camera camera;

    private void Start()
    {
        catState = CatState.idle;
        gameStartPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxColliderOrigiSize = boxCollider.size;
        startGravity = rb.gravityScale;
        startScale = transform.localScale;
        line.enabled = false;

        animator = GetComponent<Animator>();
        camera = Camera.main;
        //Debug.Log( camera.ViewportToWorldPoint(new Vector3(0, 0, 0)));
        //Debug.Log(camera.ViewportToWorldPoint(new Vector3(1, 1, 0)));
        currentEnergy = startEnergy;
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

        currentEnergy -= forceVector.magnitude;


    }


    private void Update()
    {

        switch (catState)
        {
            case CatState.idle or CatState.wallGrab:

                animator.SetBool("Jump", false);
                animator.SetBool("Fall", false);
                animator.SetBool("Run", false);
                animator.SetBool("Crouch", false);

                if (addForce)
                {
                    addForce = false;

                    animator.SetBool("WallGrab", false);

                    //Flip sprite
                    if (addForceVector.x >= 0)
                        sr.flipX = false;
                    else
                        sr.flipX = true;



                    if (addForceVector.y / force < -1 && Mathf.Abs(addForceVector.x / force) < 1)
                    {
                        catState = CatState.crouch;
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
                    catState = CatState.run;

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
                animator.SetBool("Sneak", false);

                if (Mathf.Abs(rb.linearVelocity.x) < 0.1 && Mathf.Abs(rb.linearVelocity.y) < 0.1)
                {
                    catState = CatState.idle;
                    animator.SetBool("Fall", false);
                    animator.SetBool("Run", false);
                }
                else if (Mathf.Abs(rb.linearVelocity.y) < 0.1) //Only horizontal movement
                {
                    animator.SetBool("Run", true);
                    animator.SetBool("Fall", false);
                }
                else
                {
                    animator.SetBool("Run", false);
                    animator.SetBool("Fall", true);
                }


                break;
            case CatState.ledgeGrab:
                transform.position = new Vector3(climbPos.x, climbPos.y);
                if (climbDirection == Direction.Left)
                    sr.flipX = false;
                else
                    sr.flipX = true;

                if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Cat02_LedgeGrab") && animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
                {
                    //Anim ended
                    animator.SetBool("WallGrab", false);
                    animator.SetBool("Run", false);
                    animator.SetBool("LedgeStruggle", true);
                    animator.SetBool("LedgeGrab", false);
                    catState = CatState.ledgeStruggle;
                }
                else
                {
                    animator.SetBool("LedgeGrab", true);
                }

                break;

            case CatState.ledgeStruggle:
                //transform.position = new Vector3(climbPos.x + transform.localScale.x / 2, climbPos.y + transform.localScale.y / 2);
                if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Cat02_LedgeStruggle") && animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
                {
                    //Anim ended
                    animator.SetBool("WallGrab", false);
                    animator.SetBool("LedgeStruggle", false);
                    animator.SetBool("LedgeClimb", true);
                    catState = CatState.ledgeClimb;

                }
                break;

            case CatState.ledgeClimb:
                //transform.position = new Vector3(climbPos.x + transform.localScale.x / 2, climbPos.y + transform.localScale.y / 2);
                if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Cat02_LedgeClimb") && animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
                {
                    //Anim ended
                    animator.SetBool("WallGrab", false);
                    animator.SetBool("LedgeClimb", false);
                    catState = CatState.idle;
                    if (climbDirection == Direction.Left)
                        transform.position = new Vector3(climbPos.x + transform.localScale.x / 4, climbPos.y + transform.localScale.y / 4);
                    else
                        transform.position = new Vector3(climbPos.x - transform.localScale.x / 4, climbPos.y + transform.localScale.y / 4);
                    boxCollider.enabled = true;
                }
                break;

            case CatState.walk:
                //if (Mathf.Abs(rb.linearVelocity.x) > 1)
                //    catState = CatState.run;
                //else if (Mathf.Abs(rb.linearVelocity.x) < 0.1)
                //    catState = CatState.idle;
                break;
            case CatState.run:
                animator.SetBool("Run", true);
                if (Mathf.Abs(rb.linearVelocity.x) < 0.5)
                {
                    catState = CatState.idle;
                    animator.SetBool("Run", false);
                }
                if (rb.linearVelocity.y < -0.5)
                {
                    catState = CatState.fall;
                    animator.SetBool("Run", false);
                }
                break;
            case CatState.crouch:
                //transform.localScale = new Vector3(startScale.x, startScale.y / 2, startScale.z);

                //ResetColldier();
                //sr.size = new Vector2(1f, 0.5f);
                boxCollider.size = new Vector2(boxColliderOrigiSize.x, boxColliderOrigiSize.y * 0.4f);
                animator.SetBool("Sleep", false);
                animator.SetBool("Crouch", true);
                if (addForce)
                {
                    addForce = false;
                    //Flip sprite
                    if (addForceVector.x >= 0)
                        sr.flipX = false;
                    else
                        sr.flipX = true;

                    //Disable Y movement or stand up if not allowed
                    if (keepCrouch == true)
                    {
                        addForceVector = new Vector3(addForceVector.x, 0); //y force disabled
                    }

                    if (addForceVector.y / force >= 1 && keepCrouch == false)
                    {
                        catState = CatState.idle;
                        boxCollider.size = boxColliderOrigiSize;
                        break;
                    }
                    else if (addForceVector.y / force < -1 && Mathf.Abs(addForceVector.x / force) < 1 && canSleep == true)
                    {
                        PaSoundFXManager.Instance.PlayCatSleep(transform);
                        catState = CatState.sleep;
                        break;
                    }
                    else
                    {
                        rb.AddForce(addForceVector / 2, ForceMode2D.Force);
                        addForce = false;
                    }

                }
                if (Mathf.Abs(rb.linearVelocity.x) > 0.5)
                    catState = CatState.sneak;
                break;
            case CatState.sneak:
                animator.SetBool("Sneak", true);
                if (Mathf.Abs(rb.linearVelocity.x) < 0.5)
                {
                    catState = CatState.crouch;
                    animator.SetBool("Sneak", false);
                }
                if (rb.linearVelocity.y < -0.5)
                {
                    catState = CatState.fall;
                    boxCollider.size = boxColliderOrigiSize;

                }
                break;
            case CatState.sleep:

                animator.SetBool("Sleep", true);

                if (addForce)
                {
                    addForce = false;

                    if (addForceVector.y / force >= 1)
                    {
                        catState = CatState.crouch;
                        break;
                    }


                }

                break;

            case CatState.fright:
                //transform.position = new Vector3(climbPos.x + transform.localScale.x / 2, climbPos.y + transform.localScale.y / 2);
                if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Cat02_Fright") && animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
                {
                    //Anim ended
                    animator.SetBool("Fright", false);
                    catState = CatState.crouch;
                    ResetPosition();

                }
                else
                {
                    animator.SetBool("Fright", true);
                }
                break;
            case CatState.die:
                animator.SetBool("Die", true);
                break;
            default:
                break;
        }



        if (currentEnergy < 0)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = startGravity;
            currentEnergy = 0;
            catState = CatState.die;

        }





        //Force line show
        if (line.enabled == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            endPos = ray.origin;
            line.SetPosition(0, new Vector3(endPos.x, endPos.y, 0));
            line.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 0));
            float lineLength = Vector3.Distance(line.GetPosition(0), line.GetPosition(1));
            line.startColor = Color.Lerp(Color.green, Color.red, lineLength / maxForceVectorLength);
            line.endColor = line.startColor;

            //Maximize the length
            Vector2 forceVector = transform.position - endPos;
            if (forceVector.magnitude > maxForceVectorLength)
            {
                forceVector.Normalize();
                forceVector *= maxForceVectorLength;
                line.SetPosition(0, new Vector2(transform.position.x, transform.position.y) - forceVector);
            }



        }

        //Fall down
        if (transform.position.y < -20)
        {
            ResetPosition();
        }



        if (catState != catStateLast)
        {
            Debug.Log("CatState " + catState.ToString());
            catStateLast = catState;
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fright();
        }

        //Follow the cat by cam grid
        UpdateCameraPos();

        //Move the background
        PaBackground pb = FindFirstObjectByType<PaBackground>();
        if (pb != null)
        {
            pb.cameraPosition = camera.transform.position;
            pb.referencePosition = transform.position;
        }

    }

    private void ResetPosition()
    {
        rb.gravityScale = startGravity;
        rb.linearVelocity = Vector2.zero;
        transform.position = gameStartPos;
    }

    public void Fright()
    {
        //PaSoundFXManager.Instance.PlayCatFright(transform);
        rb.gravityScale = startGravity;
        rb.linearVelocity = Vector2.zero;
        ClearAllAnimator();
        catState = CatState.fright;
        currentEnergy -= 20;
    }

    void ClearAllAnimator()
    {
        animator.SetBool("Sleep", false);
        animator.SetBool("Run", false);
        animator.SetBool("Fall", false);
        animator.SetBool("Sneak", false);
        animator.SetBool("Fright", false);
        animator.SetBool("WallGrab", false);
        animator.SetBool("LedgeGrab", false);
        animator.SetBool("LedgeStruggle", false);
        animator.SetBool("LedgeGrab", false);
    }


    void UpdateCameraPos()
    {
        //Move camera like a grid
        Vector2 lowerLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 upperRight = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        float xStep = upperRight.x - lowerLeft.x;
        float xPos = transform.position.x;
        float xFactor = Mathf.RoundToInt(xPos / xStep);
        float newX = xFactor * xStep;


        float yStep = upperRight.y - lowerLeft.y;
        float yPos = transform.position.y;
        float yFactor = Mathf.RoundToInt(yPos / yStep);
        float newY = yFactor * yStep;


        camera.transform.position = new Vector3(newX, newY, camera.transform.position.z);
        //float yStep = upperRight.y - lowerLeft.y;
        //float yFactor = (transform.position.y / (yStep / 2));
        //float newY;
        //if (yFactor < 0)
        //    newY = Mathf.CeilToInt(yFactor) * yStep;
        //else
        //    newY = Mathf.FloorToInt(yFactor) * yStep;

        //camera.transform.position = new Vector3(newX, newY, camera.transform.position.z);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PaHangable ph = collision.transform.GetComponent<PaHangable>();

        if (ph != null)
        {
            if (catState != CatState.ledgeGrab && catState != CatState.ledgeStruggle && catState != CatState.ledgeClimb && catState != CatState.idle && catState != CatState.die)
            {
                rb.linearVelocity = Vector2.zero;
                rb.gravityScale = hangGravityModifier;
                catState = CatState.wallGrab;
                animator.SetBool("WallGrab", true);
                Debug.Log("ColEnter" + ph.name);
            }
        }

        PaAnimal pa = collision.transform.GetComponent<PaAnimal>();

        if (pa != null)
        {
            Fright();

        }

        Debug.Log("ColEnter" + collision.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //rb.gravityScale = startGravity;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Climb
        PaClimbable pc = collision.transform.GetComponent<PaClimbable>();

        if (pc != null)
        {
            if (catState == CatState.jump || catState == CatState.fall)
            {
                rb.linearVelocity = Vector2.zero;
                rb.gravityScale = 0;
                boxCollider.enabled = false;
                climbDirection = pc.climbDirection;
                if (climbDirection == Direction.Left)
                    sr.flipX = false;
                else
                    sr.flipX = true;

                climbPos = pc.transform.position;
                catState = CatState.ledgeGrab;
                animator.SetBool("LedgeGrab", true);
                Debug.Log("TrgEnter" + pc.name);
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Keep crouch
        PaKeepCrouch pkc = collision.transform.GetComponent<PaKeepCrouch>();

        if (pkc != null)
        {
            keepCrouch = true;
        }

        PaSleepingPlace psp = collision.transform.GetComponent<PaSleepingPlace>();

        if (psp != null)
        {
            canSleep = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //rb.gravityScale = startGravity;
        //catState = CatState.walk;
        //Debug.Log("TrgExit");

        // Keep crouch release
        PaKeepCrouch pkc = collision.transform.GetComponent<PaKeepCrouch>();
        if (pkc != null)
        {
            keepCrouch = false;
        }

        PaSleepingPlace psp = collision.transform.GetComponent<PaSleepingPlace>();

        if (psp != null)
        {
            canSleep = false;
        }
    }

    private void OnDrawGizmos()
    {
        camera = Camera.main;
        Vector2 lowerLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 upperRight = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        Gizmos.color = Color.yellow;
        int size = 5;


        for (int i = -size; i < size; i++)
        {
            Gizmos.DrawLine(new Vector3(lowerLeft.x * i * 2 + lowerLeft.x, lowerLeft.y), new Vector3(lowerLeft.x * i * 2 + lowerLeft.x, upperRight.y));
        }

    }

}
