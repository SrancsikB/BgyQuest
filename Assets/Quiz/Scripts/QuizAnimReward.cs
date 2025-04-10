using UnityEngine;

public class QuizAnimReward : MonoBehaviour
{
    [SerializeField] float animSpeed = 1;
    [SerializeField] Sprite rewardSprite;
    bool doAnimation = false;
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, -180, 0);
        doAnimation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (doAnimation)
        {
            transform.RotateAround(transform.position, new Vector3(0, 1, 0), Time.deltaTime * animSpeed);
        }

        if (transform.rotation.eulerAngles.y >= 350)
        {
            //Finish rotation
            transform.rotation = Quaternion.Euler(0, 0, 0);
            doAnimation = false;
        }
    }
}
