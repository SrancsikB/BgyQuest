using UnityEngine;

public class QuizAnimReward : MonoBehaviour
{
    [SerializeField] float animFlipSpeed = 1;
    [SerializeField] float animCollectSpeed = 1;
    [SerializeField] ParticleSystem ps;

    public Sprite rewardSprite;
    bool doFlipAnimation = false;
    bool doCollectAnimation = false;
    void Start()
    {
        ps.gameObject.SetActive(false);
        transform.rotation = Quaternion.Euler(0, -180, 0);
        transform.gameObject.SetActive(false);
    }

    public void StartFlipAnim()
    {
        transform.gameObject.SetActive(true);
        doFlipAnimation = true;
    }


    void Update()
    {
        if (doFlipAnimation)
        {
            ps.gameObject.SetActive(true);
            transform.RotateAround(transform.position, new Vector3(0, 1, 0), Time.deltaTime * animFlipSpeed);
            if (transform.rotation.eulerAngles.y >= 260)
            {
                //Switch sprite
                GetComponent<SpriteRenderer>().sprite = rewardSprite;
            }

            if (transform.rotation.eulerAngles.y >= 350)
            {
                //Finish rotation
                transform.rotation = Quaternion.Euler(0, 0, 0);
                doFlipAnimation = false;
                
            }
        }

        if(doCollectAnimation)
        {
            
            transform.localScale -= Vector3.one * Time.deltaTime * animCollectSpeed;
            if (transform.localScale.x<0.05f)
            {
                gameObject.SetActive(false);
                doCollectAnimation = false;
                ps.gameObject.SetActive(false);
            }
        }


    }


    private void OnMouseDown()
    {
        if (doFlipAnimation == false)
            doCollectAnimation = true;
    }

}
