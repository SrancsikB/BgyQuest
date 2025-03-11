using UnityEngine;



public class RailController : MonoBehaviour
{
    public enum RailType
    {
        Up, Left, Down, Right, TurnUp, TurnLeft, TurnDown, TurnRight

    }

    public RailController.RailType railType;
    public bool isStationForTrain = false;
    //public bool isStationForWagon = false;
    public bool isStationCoalMine = false;

    private void Start()
    {
        try
        {
            //Remove arrow child if not switch
            RailSwitch railSwitch = GetComponent<RailSwitch>();
            if (railSwitch==null)
            {
                transform.GetChild(0).gameObject.SetActive(false); //Remove arrow
            }
            
        }
        catch (System.Exception)
        {
                        
        }
        
    }

    private void OnValidate()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = 10 - (int)transform.position.y;
        
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
