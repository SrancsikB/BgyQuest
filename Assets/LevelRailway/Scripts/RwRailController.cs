using UnityEngine;



public class RwRailController : MonoBehaviour
{



    public enum RailType
    {
        Up, Left, Down, Right, TurnUp, TurnLeft, TurnDown, TurnRight

    }

    public enum RailStyle
    {
        Up, Left, Down, Right,
        TurnUpFromLeft, TurnUpFromRight, TurnUpFromBoth,
        TurnDownFromLeft, TurnDownFromRight, TurnDownFromBoth,
        TurnRightFromUp, TurnRightFromDown, TurnRightFromBoth,
        TurnLeftFromUp, TurnLeftFromDown, TurnLeftFromBoth

    }


    [SerializeField] Sprite[] rails;
    [SerializeField] Sprite[] arrows;
    public RwRailController.RailStyle railStyle;
    public RwRailController.RailType railType;

    public bool isStationForTrain = false;
    //public bool isStationForWagon = false;
    public bool isStationCoalMine = false;
    public float coalMineRechargeTime = 0;
    private void Start()
    {
        try
        {

            transform.GetChild(0).gameObject.SetActive(false); //Remove arrow

        }
        catch (System.Exception)
        {

        }

    }

    private void OnValidate()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
       
        try
        {

            SpriteRenderer spRail = GetComponent<SpriteRenderer>();
            SpriteRenderer spArrow = transform.GetChild(0).GetComponent<SpriteRenderer>();
            switch (railStyle)
            {
                case RailStyle.Up:
                    spRail.sprite = rails[0];
                    spArrow.sprite = arrows[0];
                    railType = RailType.Up;
                    break;
                case RailStyle.Left:
                    spRail.sprite = rails[1];
                    spArrow.sprite = arrows[1];
                    railType = RailType.Left;
                    break;
                case RailStyle.Down:
                    spRail.sprite = rails[2];
                    spArrow.sprite = arrows[2];
                    railType = RailType.Down;
                    break;
                case RailStyle.Right:
                    spRail.sprite = rails[3];
                    spArrow.sprite = arrows[3];
                    railType = RailType.Right;
                    break;
                case RailStyle.TurnUpFromLeft:
                    spRail.sprite = rails[4];
                    spArrow.sprite = arrows[4];
                    railType = RailType.TurnUp;
                    break;
                case RailStyle.TurnUpFromRight:
                    spRail.sprite = rails[5];
                    spArrow.sprite = arrows[5];
                    railType = RailType.TurnUp;
                    break;
                case RailStyle.TurnUpFromBoth:
                    spRail.sprite = rails[6];
                    spArrow.sprite = arrows[6];
                    railType = RailType.TurnUp;
                    break;
                case RailStyle.TurnDownFromLeft:
                    spRail.sprite = rails[7];
                    spArrow.sprite = arrows[7];
                    railType = RailType.TurnDown;
                    break;
                case RailStyle.TurnDownFromRight:
                    spRail.sprite = rails[8];
                    spArrow.sprite = arrows[8];
                    railType = RailType.TurnDown;
                    break;
                case RailStyle.TurnDownFromBoth:
                    spRail.sprite = rails[9];
                    spArrow.sprite = arrows[9];
                    railType = RailType.TurnDown;
                    break;
                case RailStyle.TurnRightFromUp:
                    spRail.sprite = rails[10];
                    spArrow.sprite = arrows[10];
                    railType = RailType.TurnRight;
                    break;
                case RailStyle.TurnRightFromDown:
                    spRail.sprite = rails[11];
                    spArrow.sprite = arrows[11];
                    railType = RailType.TurnRight;
                    break;
                case RailStyle.TurnRightFromBoth:
                    spRail.sprite = rails[12];
                    spArrow.sprite = arrows[12];
                    railType = RailType.TurnRight;
                    break;
                case RailStyle.TurnLeftFromUp:
                    spRail.sprite = rails[13];
                    spArrow.sprite = arrows[13];
                    railType = RailType.TurnLeft;
                    break;
                case RailStyle.TurnLeftFromDown:
                    spRail.sprite = rails[14];
                    spArrow.sprite = arrows[14];
                    railType = RailType.TurnLeft;
                    break;
                case RailStyle.TurnLeftFromBoth:
                    spRail.sprite = rails[15];
                    spArrow.sprite = arrows[15];
                    railType = RailType.TurnLeft;
                    break;
                default:
                    break;


            }


        }
        catch (System.Exception)
        {


        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(name);
    }

    private void OnDrawGizmos()
    {

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = 10 - (int)transform.position.y;


        int X = Mathf.RoundToInt(transform.position.x);
        int Y = Mathf.RoundToInt(transform.position.y);
        transform.position = new Vector3(X, Y, 0);

        float gizmoLength = 0.40f;
        Vector3 drawFrom = Vector3.one;
        Vector3 drawTo = Vector3.one;
        Vector3 pos = transform.position;

        switch (railType)
        {
            case RailType.Up:
                drawFrom = pos - Vector3.up * gizmoLength;
                drawTo = pos + Vector3.up * gizmoLength;
                break;
            case RailType.Left:
                drawFrom = pos - Vector3.left * gizmoLength;
                drawTo = pos + Vector3.left * gizmoLength;
                break;
            case RailType.Down:
                drawFrom = pos - Vector3.down * gizmoLength;
                drawTo = pos + Vector3.down * gizmoLength;
                break;
            case RailType.Right:
                drawFrom = pos - Vector3.right * gizmoLength;
                drawTo = pos + Vector3.right * gizmoLength;
                break;
            case RailType.TurnUp:
                drawFrom = pos;
                drawTo = pos + Vector3.up * gizmoLength;
                break;
            case RailType.TurnLeft:
                drawFrom = pos;
                drawTo = pos + Vector3.left * gizmoLength;
                break;
            case RailType.TurnDown:
                drawFrom = pos;
                drawTo = pos + Vector3.down * gizmoLength;
                break;
            case RailType.TurnRight:
                drawFrom = pos;
                drawTo = pos + Vector3.right * gizmoLength;
                break;
            default:
                break;
        }
        Gizmos.color = Color.black;

        Gizmos.DrawLine(drawFrom, drawTo);
        Gizmos.DrawSphere(drawFrom, gizmoLength / 4);
        Gizmos.DrawSphere(drawTo, gizmoLength / 10);

    }
}
