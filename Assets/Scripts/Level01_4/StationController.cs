using UnityEngine;

public class StationController : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    public StationOrientation stationOrientation = StationOrientation.Up;
    public StationNames stationName = StationNames.BalassaDallas;
    public GameObject goStop;
    Vector3 startScale;
    float scaler = -0.1f;

    public enum StationNames
    {
        None, BalassaDallas, Szög, Ditroit, Palánk, Sudice, Nándor, Guta

    }

    public enum StationOrientation
    {
        Up, Left, Down, Right
    }


    private void OnValidate()
    {
        if (sprites.Length > 0)
        {

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            switch (stationOrientation)
            {
                case StationOrientation.Up:
                    sr.sprite = sprites[0];
                    break;
                case StationOrientation.Left:
                    sr.sprite = sprites[1];
                    break;
                case StationOrientation.Down:
                    sr.sprite = sprites[2];
                    break;
                case StationOrientation.Right:
                    sr.sprite = sprites[3];
                    break;
                default:
                    break;
            }
        }

    }


    private void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float scaleRate = 0.8f;
        if (goStop != null && goStop.activeSelf == true)
        {
            transform.localScale -= transform.localScale * scaler * Time.deltaTime;
            if (transform.localScale.x < scaleRate)
            {
                transform.localScale = Vector3.one * scaleRate;
                scaler *= -1;
            }
            if (transform.localScale.x > startScale.x)
            {
                transform.localScale = startScale;
                scaler *= -1;
            }
        }
    }

    private void OnDrawGizmos()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = 10 - (int)transform.position.y;
        int X = Mathf.RoundToInt(transform.position.x);
        int Y = Mathf.RoundToInt(transform.position.y);
        transform.position = new Vector3(X, Y, 0);
    }
}
