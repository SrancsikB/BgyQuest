using UnityEngine;

public class TrainControllerGrid : MonoBehaviour
{
    enum Direction
    {
        Up, Left, Down, Right //, TurningUp, TurningLeft, TurningDown, TurningRight
    }

    [SerializeField] GameObject map;
    [SerializeField] GameObject parentTrain;
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float accelerationSpeed = 0.05f;
    float angularSpeed = 90;
    float accelerationFactor = 1;
    bool activateDeceleration = false;

    [SerializeField] Smoke smoke;
    [SerializeField] float smokeFreq = 1;
    float timeToSmoke;

    TrainControllerGrid parentTrainController;
    public GameObject stations;
    public bool isWagon;
    public bool isSelectedTrain;
    public int startCoalQuantity;
    public int actualCoalQuantity;
    public bool stopMovement;
    bool releaseStopMovement; //Space key or this


    //For movement
    private float startTime;
    private float movementLength;
    public StationController.StationNames targetStation;
    StationController.StationNames reachedStation = StationController.StationNames.None;
    Vector3 startPos;
    Vector3 targetPos;
    Vector3 rotPos;
    float targetRotAngle;
    int rotateDirection = 1;
    bool tileMovementActive = false;
    bool doTurn;
    bool targetIsStation;
    Direction currentDirection;
    Direction lastDirection;


    void Start()
    {
        currentDirection = Direction.Up;
        startTime = Time.time;
        targetPos = transform.position;
        actualCoalQuantity = startCoalQuantity;

        if (isWagon)
        {
            parentTrainController = parentTrain.GetComponent<TrainControllerGrid>();
            currentDirection = parentTrainController.currentDirection;
            stations = parentTrainController.stations;
        }

        SetNextStation();

    }


    private void Update()
    {

        if (isWagon) //Use parentTrain properties if it is a wagon
        {
            targetStation = parentTrain.GetComponent<TrainControllerGrid>().targetStation;
            angularSpeed = parentTrainController.angularSpeed;
            maxSpeed = parentTrainController.maxSpeed;

        }
        else
        {
            if (activateDeceleration == false)
            {//apply more smoke during accel
                timeToSmoke += Time.deltaTime * (1 / accelerationFactor);
            }
            else
            {
                timeToSmoke += Time.deltaTime;
            }

            if (timeToSmoke >= smokeFreq && stopMovement == false)
            {
                Instantiate(smoke, transform.position, transform.rotation);
                timeToSmoke = 0;
            }
        }


        //Release movement
        if (stopMovement == true && (Input.GetKeyDown(KeyCode.Space) || releaseStopMovement))
        {
            SetNextStation();
            stopMovement = false;
            releaseStopMovement = false;
        }

        //Show target station
        foreach (Transform goStation in stations.transform)
        {
            if (goStation.GetComponent<StationController>().stationName == targetStation)
            {
                goStation.GetChild(0).gameObject.SetActive(true); //Show stop sign
            }
            else
            {
                goStation.GetChild(0).gameObject.SetActive(false);  //Hide stop sign
            }
        }


        //Coal handling
        if (!isWagon && isSelectedTrain)
        {

            FindFirstObjectByType<UICoal>().coalQuantity = actualCoalQuantity;
        }
        if (actualCoalQuantity <= 0)
        {
            this.enabled = false;
        }
    }


    void FixedUpdate()
    {
        //Acceleration
        if (!isWagon)
        {
            if (activateDeceleration == false)
            {//Accel
                if (accelerationFactor < 1)
                    accelerationFactor += accelerationSpeed;
                else
                    accelerationFactor = 1;
            }
            else
            {//Decel
                if (accelerationFactor > accelerationSpeed)
                    accelerationFactor -= accelerationSpeed;
                else
                    accelerationFactor = accelerationSpeed;
            }

        }
        else
        {
            accelerationFactor = parentTrainController.accelerationFactor;
            //Wagon movement sync
            if (accelerationFactor < accelerationSpeed * 10)
            {
                stopMovement = parentTrainController.stopMovement;
            }

        }


        if (tileMovementActive == false)
        {
            //if (isWagon)
            //{
            //    stopMovement = parentTrainController.stopMovement;
            //}

            tileMovementActive = true;

            angularSpeed = maxSpeed * 90; //+ angulerSpeedFactor;

            actualCoalQuantity -= 1;


            targetPos = GetNextTile();


            startPos = transform.position;
            if (currentDirection == Direction.Up)
            {
                targetPos += new Vector3(0, 0.5f, 0);
                rotPos = startPos + new Vector3(0, 0.5f, 0);
            }
            else if (currentDirection == Direction.Left)
            {
                targetPos += new Vector3(-0.5f, 0, 0);
                rotPos = startPos + new Vector3(-0.5f, 0, 0);
            }
            else if (currentDirection == Direction.Down)
            {
                targetPos += new Vector3(0, -0.5f, 0);
                rotPos = startPos + new Vector3(0f, -0.5f, 0);
            }
            else if (currentDirection == Direction.Right)
            {
                targetPos += new Vector3(0.5f, 0, 0);
                rotPos = startPos + new Vector3(0.5f, 0, 0);
            }


            movementLength = Vector3.Distance(startPos, targetPos);

            startTime = Time.time;
        }


        //angulerSpeed = maxSpeed * 90 + angulerSpeedFactor;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, angulerSpeed * Time.deltaTime);

        //float distCovered = (Time.time - startTime) * maxSpeed * accelerationFactor;
        //float percentOfMovement = distCovered / movementLength;

        if (doTurn)
        {
            transform.RotateAround(rotPos, new Vector3(0, 0, rotateDirection), Time.fixedDeltaTime * angularSpeed * accelerationFactor);


            if ((rotateDirection == 1 && targetRotAngle != 0 && transform.rotation.eulerAngles.z >= targetRotAngle) ||
                (rotateDirection == 1 && targetRotAngle == 0 && transform.rotation.eulerAngles.z < 30) ||
                (rotateDirection == -1 && targetRotAngle != 0 && transform.rotation.eulerAngles.z <= targetRotAngle) ||
                (rotateDirection == -1 && targetRotAngle == 0 && transform.rotation.eulerAngles.z > 330))
            {
                //Finish rotation
                transform.position = targetPos;
                transform.rotation = Quaternion.Euler(0, 0, targetRotAngle);
            }

        }
        else
        {
            if (movementLength != 0)
                //transform.position = Vector3.Lerp(startPos, targetPos, percentOfMovement);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.fixedDeltaTime * maxSpeed * accelerationFactor);
        }

        if (transform.position == targetPos && stopMovement == false)
        {
            tileMovementActive = false;
            if (targetIsStation)
            {
                //Station reached
                if (reachedStation == targetStation)
                {
                    //Stop if this station is the target
                    stopMovement = true;
                    if (!isWagon)
                    {
                        FindFirstObjectByType<GameController>().coinQuantity += 100;
                    }
                }

            }
        }






    }

    Vector3 GetNextTile()
    {
        Vector3 nextTile = Vector3.zero;
        if (currentDirection == Direction.Up)
        {
            nextTile = new Vector3(transform.position.x, Mathf.CeilToInt(transform.position.y), 0);
        }
        else if (currentDirection == Direction.Left)
        {
            nextTile = new Vector3(Mathf.FloorToInt(transform.position.x), transform.position.y, 0);
        }
        else if (currentDirection == Direction.Down)
        {
            nextTile = new Vector3(transform.position.x, Mathf.FloorToInt(transform.position.y), 0);
        }
        else if (currentDirection == Direction.Right)
        {
            nextTile = new Vector3(Mathf.CeilToInt(transform.position.x), transform.position.y, 0);
        }




        foreach (Transform goTile in map.transform)
        {
            if (goTile.position == nextTile)
            {
                RailController.RailType railType = goTile.GetComponent<RailController>().railType;
                if (!isWagon)
                {
                    targetIsStation = goTile.GetComponent<RailController>().isStationForTrain;

                }
                //else
                //  targetIsStation = goTile.GetComponent<RailController>().isStationForWagon;

                if (targetIsStation)
                {
                    reachedStation = goTile.GetComponent<StationController>().stationName;
                    if (reachedStation == targetStation)
                    {
                        activateDeceleration = true;
                    }
                }
                else
                    reachedStation = StationController.StationNames.None;

                if (goTile.GetComponent<RailController>().isStationCoalMine == true)
                {
                    actualCoalQuantity += 100;
                    if (actualCoalQuantity > startCoalQuantity)
                        actualCoalQuantity = startCoalQuantity;
                }

                rotateDirection = 1;
                switch (railType)
                {
                    case RailController.RailType.Up:
                        currentDirection = Direction.Up;
                        doTurn = false;
                        break;

                    case RailController.RailType.Left:
                        currentDirection = Direction.Left;
                        doTurn = false;
                        break;

                    case RailController.RailType.Down:
                        currentDirection = Direction.Down;
                        doTurn = false;
                        break;

                    case RailController.RailType.Right:
                        currentDirection = Direction.Right;
                        doTurn = false;
                        break;


                    case RailController.RailType.TurnUp:
                        if (lastDirection == Direction.Up)  //Skip turn if direction is same as before
                        {
                            currentDirection = Direction.Up;
                            break;
                        }
                        if (lastDirection == Direction.Left) rotateDirection = -1;
                        targetRotAngle = 0;
                        currentDirection = Direction.Up;
                        doTurn = true;
                        break;

                    case RailController.RailType.TurnLeft:
                        if (lastDirection == Direction.Left)  //Skip turn if direction is same as before
                        {
                            currentDirection = Direction.Left;
                            break;
                        }
                        if (lastDirection == Direction.Down) rotateDirection = -1;
                        targetRotAngle = 90;
                        currentDirection = Direction.Left;
                        doTurn = true;
                        break;

                    case RailController.RailType.TurnDown:
                        if (lastDirection == Direction.Down)  //Skip turn if direction is same as before
                        {
                            currentDirection = Direction.Down;
                            break;
                        }
                        if (lastDirection == Direction.Right) rotateDirection = -1;
                        targetRotAngle = 180;
                        currentDirection = Direction.Down;
                        doTurn = true;
                        break;

                    case RailController.RailType.TurnRight:
                        if (lastDirection == Direction.Right)  //Skip turn if direction is same as before
                        {
                            currentDirection = Direction.Right;
                            break;
                        }
                        if (lastDirection == Direction.Up) rotateDirection = -1;
                        targetRotAngle = 270;
                        currentDirection = Direction.Right;
                        doTurn = true;
                        break;


                    default:
                        break;
                }

                lastDirection = currentDirection;


                return goTile.position;
            }
        }


        return Vector3.zero;
    }


    public void SetNextStation()
    {


        if (!isWagon)
        {
            accelerationFactor = accelerationSpeed;
            activateDeceleration = false;
            StationController.StationNames newRandomStation = targetStation;

            while (newRandomStation == targetStation) //Avoid same seed
            {
                int index = Random.Range(0, stations.transform.childCount);
                newRandomStation = stations.transform.GetChild(index).GetComponent<StationController>().stationName;

            }
            targetStation = newRandomStation;
        }




    }

    public void OnMouseDown()
    {
        SelectTrain();

    }

    public void SelectTrain()
    {
        if (!isWagon)
        {
            TrainControllerGrid[] trains = FindObjectsByType<TrainControllerGrid>(FindObjectsSortMode.None);
            foreach (TrainControllerGrid item in trains)
            {
                item.isSelectedTrain = false;
                item.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
            isSelectedTrain = true;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.0f, 1f);

            //Release movement
            if (stopMovement == true)
            {
                //releaseStopMovement = true;
                SetNextStation();
                stopMovement = false;
            }
        }
    }



    private void OnDrawGizmos()
    {

        //int X = Mathf.RoundToInt(transform.position.x);
        //int Y = Mathf.RoundToInt(transform.position.y);
        //transform.position = new Vector3(X, Y-0.5f, 0);
    }
}
