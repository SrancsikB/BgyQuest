using UnityEngine;

public class PaSpike : MonoBehaviour
{
    [SerializeField] float loweringSpeed = 0.01f;
    [SerializeField] float zToFall = 1.8f;
    [SerializeField] Sprite startSprite;
    [SerializeField] float startDelayMin = 1.0f;
    [SerializeField] float startDelayMax = 3.0f;

    float timeToFall;

    Vector3 posToFall;
    Vector3 startPos;
    Rigidbody2D rb;
    float startGravity;
    SpriteRenderer sr;
    //BoxCollider2D boxCollider;
    Animator animator;


    void InitSpike()
    {
        timeToFall = Random.Range(startDelayMin, startDelayMax);
        sr.sprite = startSprite;
        animator.SetBool("Crash", false);
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
        //boxCollider.enabled = false;
        transform.position = startPos;
        posToFall = transform.position + Vector3.down * zToFall;
    }

    void Start()
    {
        //boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        startGravity = rb.gravityScale;
        startPos = transform.position;
        InitSpike();

    }


    void Update()
    {
        timeToFall -= Time.deltaTime;

        if (transform.position.y <= posToFall.y)
        {
            //Falling till crash
            rb.gravityScale = startGravity;
            //boxCollider.enabled = true;
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Spike_Crash") && animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                //Anim ended
                InitSpike();
            }
        }
        else
        {
            if (timeToFall < 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, posToFall, Time.deltaTime * loweringSpeed);
            }

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.gravityScale > 0)
        {
            animator.SetBool("Crash", true);
        }

    }




}
