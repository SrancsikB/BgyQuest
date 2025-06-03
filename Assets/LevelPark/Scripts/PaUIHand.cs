using UnityEngine;

public class PaUIHand : MonoBehaviour
{
    enum State
    {
        Find, Hold, Move, Release
    }

    enum HandMovement
    {
        Run, Jump, Crouch, StandUp

    }

    [SerializeField] HandMovement handMovement;
    [SerializeField] Transform target;
    [SerializeField] float speed = 1;
    [SerializeField] Sprite released;
    [SerializeField] Sprite hold;
    [SerializeField] GameObject pushLine;
    Vector3 startPos;
    Vector3 endPos;
    float waitTime = 1;
    SpriteRenderer sr;
    LineRenderer line;

    State state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = target.position;
        switch (handMovement)
        {
            case HandMovement.Run:
                endPos = startPos + Vector3.left * 2;
                break;
            case HandMovement.Jump:
                endPos = startPos + Vector3.left * 1 + Vector3.down * 1;
                break;
            case HandMovement.Crouch:
                endPos = startPos + Vector3.up * 1.5f;
                break;
            case HandMovement.StandUp:
                endPos = startPos + Vector3.down * 1.5f;
                break;
            default:
                break;
        }
        
        
        transform.position = endPos;
        state = State.Find;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = released;

        line=Instantiate(pushLine).GetComponent<LineRenderer>();

        line.SetPosition(0, startPos);
        line.SetPosition(1, startPos);
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Find:
                transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * speed);
                if (transform.position == startPos)
                {
                    waitTime = 0.5f;
                    state = State.Hold;
                }

                break;
            case State.Hold:
               
                sr.sprite = hold;
                waitTime -= Time.deltaTime;
                if (waitTime < 0)
                {
                    state = State.Move;
                }
                break;
            case State.Move:
                transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);

                line.enabled = true;
                line.SetPosition(0, new Vector2(transform.position.x, transform.position.y));
                float lineLength = Vector3.Distance(line.GetPosition(0), line.GetPosition(1));
                line.startColor = Color.Lerp(Color.green, Color.red, lineLength);
                line.endColor = line.startColor;

                if (transform.position == endPos)
                {
                    waitTime = 0.5f;
                    state = State.Release;
                }
                break;
            case State.Release:
                line.SetPosition(0, startPos);
                line.enabled = false;
                sr.sprite = released;
                waitTime -= Time.deltaTime;
                if (waitTime < 0)
                {
                    state = State.Find;
                }
                break;
            default:
                break;
        }
    }
}
