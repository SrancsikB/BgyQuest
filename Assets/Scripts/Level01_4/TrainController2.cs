using UnityEngine;

public class TrainController2 : MonoBehaviour
{
    [SerializeField] float angulerSpeed = 90;
    [SerializeField] float angulerSpeedFactor = 1;
    [SerializeField] float maxSpeed = 10;
    //[SerializeField] float accel = 10;
    //public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    public RwStationController.StationNames nextStation;


    Quaternion targetRot;
    Vector3 startPos;
    Vector3 targetPos;
    bool doSlerp;
    Collider2D lastRailUnder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = Time.time;
        targetRot = transform.rotation;
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        angulerSpeed = maxSpeed * 90+ angulerSpeedFactor;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, angulerSpeed * Time.deltaTime);

        float distCovered = (Time.time - startTime) * maxSpeed;
        float fractionOfJourney = distCovered / journeyLength;

        if (doSlerp)
        {
            transform.position = Vector3.Slerp(startPos, targetPos, fractionOfJourney);
        }
        else
        {
            transform.position = Vector3.Lerp(startPos, targetPos, fractionOfJourney);
        }

        //railUnder.transform.Rotate(Vector3.forward, fractionOfJourney/angulerSpeed);

        //if (transform.rotation == targetRot)
        //{

        //    if (Input.GetKeyDown(KeyCode.RightArrow))
        //    {
        //        targetRot = Quaternion.AngleAxis(transform.rotation.eulerAngles.z - 90, transform.forward);

        //    }
        //}

        //transform.position = Vector3.MoveTowards(transform.position, targetPos, maxSpeed * Time.deltaTime);





    }


    void OnTriggerEnter2D(Collider2D other)
    {
        RwRailController.RailType railType = other.GetComponent<RwRailController>().railType;
        Vector3 railLocation = other.transform.position;
        int rotateDirection = 1;
        switch (railType)
        {
            case RwRailController.RailType.Up:
                
                targetPos = railLocation + Vector3.up;
                doSlerp = false;
                break;
            case RwRailController.RailType.Down:
                targetPos = railLocation + Vector3.down;
                doSlerp = false;
                break;
            case RwRailController.RailType.Left:
                targetPos = railLocation + Vector3.left;
                doSlerp = false;
                break;
            case RwRailController.RailType.Right:
                targetPos = railLocation + Vector3.right;
                doSlerp = false;
                break;

            case RwRailController.RailType.TurnDown:
                if (lastRailUnder.GetComponent<RwRailController>().railType == RwRailController.RailType.TurnRight) rotateDirection = -1;
                targetPos = railLocation + Vector3.down * 0.5f;
                targetRot = Quaternion.AngleAxis(transform.rotation.eulerAngles.z + 90 * rotateDirection, transform.forward);
                doSlerp = true;
                break;

            case RwRailController.RailType.TurnUp:
                targetPos = railLocation + Vector3.up * 0.5f;
                targetRot = Quaternion.AngleAxis(transform.rotation.eulerAngles.z + 90 * rotateDirection, transform.forward);
                doSlerp = true;
                break;

            case RwRailController.RailType.TurnLeft:
                targetPos = railLocation + Vector3.left * 0.5f;
                targetRot = Quaternion.AngleAxis(transform.rotation.eulerAngles.z + 90 * rotateDirection, transform.forward);
                doSlerp = true;
                break;

            case RwRailController.RailType.TurnRight:
                if (lastRailUnder.GetComponent<RwRailController>().railType == RwRailController.RailType.TurnDown) rotateDirection = -1;
                targetPos = railLocation + Vector3.right * 0.5f;
                targetRot = Quaternion.AngleAxis(transform.rotation.eulerAngles.z + 90, transform.forward);
                doSlerp = true;
                break;

            default:
                break;
        }
        lastRailUnder = other;
        startTime = Time.time;
        startPos = transform.position;
        journeyLength = Vector3.Distance(startPos, targetPos);
        Debug.Log(other.GetComponent<RwRailController>().railType);

        //other.transform.localScale=Vector3.one*0.97f;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //other.transform.localScale = Vector3.one * 1f;
        //other.transform.rotation = Quaternion.Euler(Vector3.zero);

    }

}



/*
 
public class SpaceShipController : MonoBehaviour
{

    [SerializeField] float angulerSpeed = 90;
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float accel = 10;
    [SerializeField] float drag = 0.5f;

    [SerializeField] Projectile projectile;
        

    Vector3 velocity;

    void Update()
    {

        //Shoot--------------------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Projectile newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            newProjectile.SetStartVelocity(velocity);
        }


        float rotationInput = Input.GetAxisRaw("Horizontal");
        transform.Rotate(0, 0, -rotationInput * angulerSpeed * Time.deltaTime);

        transform.position += velocity * Time.deltaTime;


    }

    void FixedUpdate()
    {




        //Accel handling-------------------

        float movementInput = Input.GetAxisRaw("Vertical");

        if (movementInput < 0) movementInput = 0;
        Vector3 accelVector = transform.up * movementInput * this.accel;
        velocity += accelVector * Time.fixedDeltaTime;
        Vector3 dragVector = velocity * drag;
        velocity -= dragVector * Time.fixedDeltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

    }
}

 * 
*/