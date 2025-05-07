using UnityEngine;

public class RwStationController : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    public StationOrientation stationOrientation = StationOrientation.Up;
    public StationNames stationName = StationNames.Balassagyarmat;
    StationColor stationStopColor;
    public GameObject goStopRed;
    public GameObject goStopBlue;
    public GameObject goStopGreen;
    public GameObject goStopYellow;
    public GameObject goStopPurple;
    Vector3 startScale;
    float scaler = -0.1f;

    public enum StationNames
    {
        None, Balassagyarmat, Orhalom, Ipolyszog, Dejtar, Degelypalank, Szugy, Magyarnandor, Galgaguta,
        Hall, Park, Hospital,
        Kaposvar, Pecs, Szeged, Szolnok, Bekescsaba, Eger, Miskolc, Nyiregyhaza, Debrecen,
        Paris, Berlin, Praha, Wien, Belgrad, Rome, Budapest

    }

    public enum StationOrientation
    {
        Up, Left, Down, Right
    }

    public enum StationColor
    {
        Red, Blue, Green, Yellow, Purple
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
        //Bumping effect
        if (goStopRed.activeSelf || goStopBlue.activeSelf || goStopGreen.activeSelf || goStopYellow.activeSelf || goStopPurple.activeSelf) //if target of any train
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
        else
        {
            transform.localScale = startScale;
        }
    }

    public void ShowStopSign(StationColor stationColor, bool isShow)
    {
        switch (stationColor)
        {
            case StationColor.Red:
                goStopRed.SetActive(isShow);
                break;
            case StationColor.Blue:
                goStopBlue.SetActive(isShow);
                break;
            case StationColor.Green:
                goStopGreen.SetActive(isShow);
                break;
            case StationColor.Yellow:
                goStopYellow.SetActive(isShow);
                break;
            case StationColor.Purple:
                goStopPurple.SetActive(isShow);
                break;
            default:
                break;
        }
    }

    public void HideAllStopSign()
    {
        goStopRed.SetActive(false);
        goStopBlue.SetActive(false);
        goStopGreen.SetActive(false);
        goStopYellow.SetActive(false);
        goStopPurple.SetActive(false);
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
