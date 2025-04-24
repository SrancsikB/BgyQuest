using UnityEngine;

public class RwTrainControllerGrid : MonoBehaviour
{
    enum Direction
    {
        Up, Left, Down, Right //, TurningUp, TurningLeft, TurningDown, TurningRight
    }

    //Train map and speed, setup by Railway level
    
    [SerializeField] GameObject map;
    public GameObject stations;
    public RwStationController.StationColor stationColor;
    [SerializeField] GameObject parentTrain;
    [SerializeField] GameObject coalHeap;
    [SerializeField] GameObject coinHeap;

    [SerializeField] float accelerationSpeed = 0.05f;
    float angularSpeed = 90;
    float accelerationFactor = 1;
    bool activateDeceleration = false;

    //Fx
    [SerializeField] RwSmoke smoke;
    [SerializeField] float smokeFreq = 1;
    float timeToSmoke;
    [SerializeField] GameObject flameAnim;

    //Behaviour
    RwTrainControllerGrid parentTrainController;
    public bool isWagon;

    public bool isSelectedTrain;


    //Setup by GameController
    public float maxSpeed = 1;
    public int startCoalQuantity;
    int actualCoalQuantity;
    public int minReward;
    public int maxReward;
    int bonusReward;
    public float maxTimeToDecreaseReward;
    float timeToDecreaseReward;

    public int wagonNumber;

    public float coalHeapShowTime;
    float coalHeapShowActTimer;
    public float coalHeapHideTime;
    float coalHeapHideActTimer;
    public int coalHeapQuantity;

   

    public bool stopMovement;
    bool releaseStopMovement; //Space key or set this to true


    //For movement
    private float movementLength;
    public RwStationController.StationNames targetStation;
    RwStationController.StationNames reachedStation = RwStationController.StationNames.None;
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
    int wagonDecelSyncCounter = 0;

    void Start()
    {
        currentDirection = Direction.Up;
        targetPos = transform.position;


        if (isWagon)
        {
            parentTrainController = parentTrain.GetComponent<RwTrainControllerGrid>();
            currentDirection = parentTrainController.currentDirection;
            stations = parentTrainController.stations;
        }
        else
        {
            actualCoalQuantity = startCoalQuantity;
            bonusReward = maxReward;
            timeToDecreaseReward = maxTimeToDecreaseReward;
            coalHeapShowActTimer = coalHeapShowTime;
            coalHeapHideActTimer = coalHeapHideTime;
            coalHeap = Instantiate(coalHeap);
            CoalHeapRandomize(coalHeap);

            //Coinheap init
            RwGameController gc = FindFirstObjectByType<RwGameController>();

            RwCoinHeapController ch = Instantiate(coinHeap).GetComponent<RwCoinHeapController>();
            ch.map = map;
            ch.coinHeapShowTime = gc.coinHeapShowTime;
            ch.coinHeapHideTime = gc.coinHeapHideTime;
            ch.coinHeapQuantity = gc.coinHeapQuantity;
            ch.CoinHeapRandomize();

        }

        SetNextStation();

        
    }


    private void Update()
    {

        if (isWagon) //Use parentTrain properties if it is a wagon
        {
            targetStation = parentTrain.GetComponent<RwTrainControllerGrid>().targetStation;
            angularSpeed = parentTrainController.angularSpeed;
            maxSpeed = parentTrainController.maxSpeed;
            this.enabled = parentTrain.GetComponent<RwTrainControllerGrid>().enabled;

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

            //Out of coal
            if (actualCoalQuantity <= 0)
            {
                RwSoundFXManager.Instance.PlayTrainStopEmpty(transform);
                this.enabled = false;
            }


            //Bonus reward handling
            timeToDecreaseReward -= Time.deltaTime;
            if (timeToDecreaseReward < 0)
            {
                if (bonusReward > 0)
                    bonusReward -= 1; //Decrease bonus
                timeToDecreaseReward = maxTimeToDecreaseReward;
            }


            //Coal heap show / hide
            coalHeapShowActTimer -= Time.deltaTime;
            if (coalHeapShowActTimer < 0)
            {
                coalHeap.SetActive(true);
                coalHeapHideActTimer -= Time.deltaTime;
                if (coalHeapHideActTimer < 0)
                {
                    //coalHeap.SetActive(false);
                    CoalHeapRandomize(coalHeap);
                    coalHeapShowActTimer = coalHeapShowTime;
                    coalHeapHideActTimer = coalHeapHideTime;
                }
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
        //foreach (Transform goStation in stations.transform)
        //{
        //    if (goStation.GetComponent<RwStationController>().stationName == targetStation)
        //    {
        //        goStation.GetChild(0).gameObject.SetActive(true); //Show stop sign
        //    }
        //    else
        //    {
        //        goStation.GetChild(0).gameObject.SetActive(false);  //Hide stop sign
        //    }
        //}




        //Coal handling and UI in foot
        if (!isWagon && isSelectedTrain)
        {

            FindFirstObjectByType<UICoal>().UpdateValue(actualCoalQuantity);
            FindFirstObjectByType<UIStation>().UpdateValue(targetStation.ToString());
            FindFirstObjectByType<UIReward>().UpdateValue(minReward + bonusReward);
        }

    }


    public void FixedUpdateRemote()
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
            if (accelerationFactor < 0.9f)//decel started by train
            {
                wagonDecelSyncCounter += 1;
                if (wagonDecelSyncCounter > 40 / maxSpeed) //Totally experience value...
                    stopMovement = parentTrainController.stopMovement;
            }
            else
            {
                wagonDecelSyncCounter = 0;
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

            actualCoalQuantity -= 1; //Decrease coal



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
                        RwGameController gc = FindFirstObjectByType<RwGameController>();

                        gc.coinQuantity += minReward + bonusReward; //Temp 100
                        bonusReward = maxReward;
                        RwSoundFXManager.Instance.PlayTrainStop(transform);
                        try
                        {
                            PlayerDataControl.Instance.SaveCoinData(gc.coinQuantity);
                        }
                        catch (System.Exception)
                        {
                            Debug.Log("Coin save failed");
                        }

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
                RwRailController.RailType railType = goTile.GetComponent<RwRailController>().railType;
                if (!isWagon)
                {
                    if (goTile.GetComponent<RwRailController>().stationToStartDecel != RwStationController.StationNames.None)
                        targetIsStation = true;
                    else
                        targetIsStation = false;

                }
                //else
                //  targetIsStation = goTile.GetComponent<RailController>().isStationForWagon;

                if (targetIsStation)
                {
                    reachedStation = goTile.GetComponent<RwRailController>().stationToStartDecel;
                    if (reachedStation == targetStation)
                    {
                        activateDeceleration = true;
                    }
                }
                else
                    reachedStation = RwStationController.StationNames.None;

                //if (goTile.GetComponent<RailController>().isStationCoalMine == true)
                //{
                //    int coalMineLevel = PlayerDataControl.Instance.GetRailwayCoalMineLevel;
                //    switch (coalMineLevel)
                //    {
                //        case 1:
                //            actualCoalQuantity += Random.Range(10, 20);
                //            break;
                //        case 2:
                //            actualCoalQuantity += Random.Range(20, 50);
                //            break;
                //        case 3:
                //            actualCoalQuantity += Random.Range(50, 100);
                //            break;
                //        default:
                //            break;
                //    }

                //    if (actualCoalQuantity > startCoalQuantity)
                //        actualCoalQuantity = startCoalQuantity;
                //}

                rotateDirection = 1;
                switch (railType)
                {
                    case RwRailController.RailType.Up:
                        currentDirection = Direction.Up;
                        doTurn = false;
                        break;

                    case RwRailController.RailType.Left:
                        currentDirection = Direction.Left;
                        doTurn = false;
                        break;

                    case RwRailController.RailType.Down:
                        currentDirection = Direction.Down;
                        doTurn = false;
                        break;

                    case RwRailController.RailType.Right:
                        currentDirection = Direction.Right;
                        doTurn = false;
                        break;


                    case RwRailController.RailType.TurnUp:
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

                    case RwRailController.RailType.TurnLeft:
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

                    case RwRailController.RailType.TurnDown:
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

                    case RwRailController.RailType.TurnRight:
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
            RwStationController.StationNames newRandomStation = targetStation;

            while (newRandomStation == targetStation) //Avoid same seed
            {
                int index = Random.Range(0, stations.transform.childCount);
                newRandomStation = stations.transform.GetChild(index).GetComponent<RwStationController>().stationName;

            }
            targetStation = newRandomStation;

            RwSoundFXManager.Instance.PlayTrainStart(transform);

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
            DeselectTrains();
            Animator anim; //Only the selection sprite has anim

            isSelectedTrain = !isSelectedTrain;

            anim = GetComponentInChildren<Animator>(true);
            if (anim != null)
                anim.gameObject.SetActive(isSelectedTrain);

            if (isSelectedTrain == false)
            {
                FindFirstObjectByType<UICoal>().ClearValue();
                FindFirstObjectByType<UIStation>().ClearValue();
                FindFirstObjectByType<UIReward>().ClearValue();
            }

            //Release movement
            if (stopMovement == true)
            {
                //releaseStopMovement = true;
                SetNextStation();
                stopMovement = false;
            }
        }
    }


    public void DeselectTrains()
    {
        Animator anim; //Only the selection sprite has anim
        RwTrainControllerGrid[] trains = FindObjectsByType<RwTrainControllerGrid>(FindObjectsSortMode.None);
        foreach (RwTrainControllerGrid train in trains)
        {
            if (train != this)
            {
                train.isSelectedTrain = false;
                anim = train.GetComponentInChildren<Animator>(true);
                if (anim != null)
                    anim.gameObject.SetActive(false);
            }
        }
    }

    private void CoalHeapRandomize(GameObject coalHeap)
    {
        int rndMapTile = Random.Range(0, map.transform.childCount);
        coalHeap.transform.position = map.transform.GetChild(rndMapTile).position;
        coalHeap.SetActive(false); //Will show after timer 
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isWagon)
        {


            if (collision.gameObject.GetComponent<RwCoalHeapController>() != null)
            {
                actualCoalQuantity += coalHeapQuantity;
                coalHeapShowActTimer = coalHeapShowTime;
                coalHeapHideActTimer = coalHeapHideTime;
                CoalHeapRandomize(collision.gameObject);

            }
            if (collision.gameObject.GetComponent<RwCoinHeapController>() != null)
            {
                RwGameController gc = FindFirstObjectByType<RwGameController>();
                RwCoinHeapController coinHC = collision.gameObject.GetComponent<RwCoinHeapController>();
                gc.coinQuantity += coinHC.coinHeapQuantity;
                
                coinHC.CoinHeapRandomize();
                try
                {
                    PlayerDataControl.Instance.SaveCoinData(gc.coinQuantity);
                }
                catch (System.Exception)
                {
                    Debug.Log("Coin save failed");
                }

            }

            if (collision.gameObject.GetComponent<RwTrainControllerGrid>() != null)
            {
                RwTrainControllerGrid otherTrain = collision.gameObject.GetComponent<RwTrainControllerGrid>();
                if (otherTrain.isWagon == false)
                {
                    Debug.Log("Crash train");
                    RwSoundFXManager.Instance.PlayTrainStopEmpty(transform);
                    this.enabled = false;
                    otherTrain.enabled = false;
                    Instantiate(flameAnim, transform);
                    Instantiate(flameAnim, otherTrain.transform);
                }
                else if (otherTrain.parentTrain.name != name) // This should work, but NOK at start: else if (otherTrain.parentTrain != this)
                {
                    Debug.Log("Crash other wagon");
                    RwSoundFXManager.Instance.PlayTrainStopEmpty(transform);
                    this.enabled = false;
                    otherTrain.parentTrain.GetComponent<RwTrainControllerGrid>().enabled = false;
                    Instantiate(flameAnim, transform);
                    Instantiate(flameAnim, otherTrain.transform);
                }
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
