using UnityEngine;

public class RwGameController : MonoBehaviour
{
    public int coinQuantity;

    //RV specific
    [SerializeField] PlayerDataControl.RailwayPogressData rwPD;
    [SerializeField] Sprite outOfCoalTrainSprite;
    [SerializeField] Sprite outOfCoalWagonSprite;
    [SerializeField] GameObject stations;
    [SerializeField] GameObject gameOverUI;

    public float globalSwitchingTime;

    public float globalTrainSpeed;

    public int globalCoalQuantity;
    public int globalWagonLevel;

    public int transportMinReward;
    public int transportMaxReward;
    public float transportTimeToDecreaseReward;

    public float coalHeapShowTime;
    public float coalHeapHideTime;
    public int coalHeapQuantity;

    public float coinHeapShowTime;
    public float coinHeapHideTime;
    public int coinHeapQuantity;

    private void Awake()
    {


        try //try to get Player data
        {
            PlayerDataControl.Instance.lockQuiz = false; //New game started, unlock quiz
            PlayerDataControl pDC = PlayerDataControl.Instance;
            coinQuantity = pDC.coins;
            rwPD = pDC.GetRailwayProgressData();


        }
        catch (System.Exception)
        {
            coinQuantity = 0;
            Debug.Log("SB: Player data cannot be aquired, using public progress data");
        }

        globalTrainSpeed = 0.5f + (float)rwPD.rwTrainSpeed * 0.5f;
        globalSwitchingTime = 6 - rwPD.rwSwitchingTime;
        globalWagonLevel = rwPD.rwWagonLevel;
        globalCoalQuantity = 50 + rwPD.rwWagonLevel * 50;
        transportMinReward = 15 + rwPD.rwWagonLevel * 15;
        transportMaxReward = 25 + rwPD.rwWagonLevel * 25;

        coalHeapQuantity = 10 + rwPD.rwCoalHeapLevel * 10;
        coalHeapShowTime = 60 / rwPD.rwCoalHeapLevel;
        coalHeapHideTime = rwPD.rwCoalHeapLevel * 10;

        coinHeapQuantity = 20 + rwPD.rwBonusCoinLevel * 20;
        coinHeapShowTime = 60 / rwPD.rwBonusCoinLevel;
        coinHeapHideTime = rwPD.rwBonusCoinLevel * 10;
        transportTimeToDecreaseReward = rwPD.rwBonusCoinLevel;

        //Setup start params
        RwRailSwitch[] rs = FindObjectsByType<RwRailSwitch>(FindObjectsSortMode.None);
        foreach (var railSwitch in rs)
        {
            railSwitch.switchingTime = globalSwitchingTime;
        }

        RwTrainControllerGrid[] trains = FindObjectsByType<RwTrainControllerGrid>(FindObjectsSortMode.None);
        //trains[0].SelectTrain(); //Select 1st train by default
        foreach (RwTrainControllerGrid train in trains)
        {
            train.maxSpeed = globalTrainSpeed;
            train.startCoalQuantity = globalCoalQuantity;
            train.minReward = transportMinReward;
            train.maxReward = transportMaxReward;
            train.maxTimeToDecreaseReward = transportTimeToDecreaseReward;
            train.coalHeapShowTime = coalHeapShowTime;
            train.coalHeapHideTime = coalHeapHideTime;
            train.coalHeapQuantity = coalHeapQuantity;
            //train.coinHeapShowTime = coinHeapShowTime;
            //train.coinHeapHideTime = coinHeapHideTime;
            //train.coinHeapQuantity = coinHeapQuantity;
            if (train.wagonNumber > globalWagonLevel)
            {
                train.gameObject.SetActive(false);
            }
            if (train.wagonNumber == globalWagonLevel)
            {
                RwChainController chain = train.transform.GetComponentInChildren<RwChainController>();
                if (chain != null)
                    chain.gameObject.SetActive(false);
            }

            //Turn sprite if out of coal
            //if (train.enabled == false)
            //{
            //    if (!train.isWagon)
            //        train.GetComponent<SpriteRenderer>().sprite = outOfCoalTrainSprite;
            //    else
            //        train.GetComponent<SpriteRenderer>().sprite = outOfCoalWagonSprite;
            //}
        }





        //wagon.GetComponent<TrainControllerGrid>().startCoalQuantity = globalCoalQuantity;

        //coinQuantity = startCoinQuantity;

        //SetNextStation();
    }

    private void FixedUpdate()
    {
        bool isGameOver = true; //All train is disabled, crashed or out of coal
        //Make sure that all train updates first
        RwTrainControllerGrid[] trains = FindObjectsByType<RwTrainControllerGrid>(FindObjectsSortMode.None);
        foreach (RwTrainControllerGrid train in trains)
        {

            if (train.isWagon == false)
            {
                if (train.enabled)
                {
                    train.FixedUpdateRemote();
                    isGameOver = false;
                }
                else
                    train.GetComponent<SpriteRenderer>().sprite = outOfCoalTrainSprite;
            }

        }

        //And wagons later
        RwTrainControllerGrid[] wagons = FindObjectsByType<RwTrainControllerGrid>(FindObjectsSortMode.None);
        foreach (RwTrainControllerGrid wagon in wagons)
        {

            if (wagon.isWagon == true)
            {
                if (wagon.enabled)
                    wagon.FixedUpdateRemote();
                else
                    wagon.GetComponent<SpriteRenderer>().sprite = outOfCoalWagonSprite;
            }

        }

        if (isGameOver)
        {
            gameOverUI.SetActive(true);
        }

    }

    void Update()
    {

        FindFirstObjectByType<UICoin>().coinQuantity = coinQuantity;


        //Show target stations

        foreach (Transform goStation in stations.transform)
        {
            goStation.GetComponent<RwStationController>().HideAllStopSign();  //Hide stop sign
        }

        foreach (Transform goStation in stations.transform)
        {

            RwTrainControllerGrid[] trains = FindObjectsByType<RwTrainControllerGrid>(FindObjectsSortMode.None);
            foreach (RwTrainControllerGrid train in trains)
            {

                if (train.isWagon == false)
                {
                    if (train.enabled)
                    {
                        if (goStation.GetComponent<RwStationController>().stationName == train.targetStation)
                        {
                            goStation.GetComponent<RwStationController>().ShowStopSign(train.stationColor, true); //Show stop sign
                        }
                    }
                }
            }

        }


        //Coin heap
        RwCoinHeapController[] coinHeaps = FindObjectsOfType<RwCoinHeapController>(true);
            
        foreach (RwCoinHeapController coinHeap in coinHeaps)
        {
            coinHeap.UpdateRemote();
        }
    }
}



