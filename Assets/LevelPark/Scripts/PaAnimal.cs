using UnityEngine;

public class PaAnimal : MonoBehaviour
{
    enum AnimalState
    {
        idle, sit, action
    }


    [SerializeField] float timeToFlip = 3.0f;
    [SerializeField] float timeToSit = 7.0f;
    [SerializeField] float timeOfAction = 3.0f;
    float timerToFlip;
    float timerToSit;
    float timerOfAction;

    BoxCollider2D boxCollider;
    Animator animator;

    private void Start()
    {

        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (animator.GetBool("Action") == true )
        {
            
            timerOfAction += Time.deltaTime;
            if (timerOfAction > timeOfAction)
            {
                animator.SetBool("Action", false);
                timerOfAction = 0;
            }
            
        }
        else 
        {


            timerToFlip += Time.deltaTime;
            if (timerToFlip > timeToFlip)
            {
                //flip
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                timerToFlip = 0;
            }

            timerToSit += Time.deltaTime;
            if (timerToSit > timeToSit)
            {
                animator.SetBool("Sit", !animator.GetBool("Sit"));
                timerToSit = 0;
            }

        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PaCat paCat = collision.transform.GetComponent<PaCat>();
        if (paCat != null)
        {
            paCat.Fright();
            animator.SetBool("Action", true);
        }

    }
}
