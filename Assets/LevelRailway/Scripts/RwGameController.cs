using UnityEngine;

public class RwGameController : MonoBehaviour
{
    public int coinQuantity;

    //RV specific
    [SerializeField] PlayerDataControl.RailwayPogressData rwPD;
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

    private void Start()
    {


        try //try to get Player data
        {
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
        trains[0].SelectTrain(); //Select 1st train by default
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
            train.coinHeapShowTime = coinHeapShowTime;
            train.coinHeapHideTime = coinHeapHideTime;
            train.coinHeapQuantity = coinHeapQuantity;
            if (train.wagonNumber>globalWagonLevel)
            {
                train.gameObject.SetActive(false);
            }
            if (train.wagonNumber==globalWagonLevel)
            {
                RwChainController chain = train.transform.GetComponentInChildren<RwChainController>();
                if (chain!=null)
                    chain.gameObject.SetActive(false);
            }
        }





        //wagon.GetComponent<TrainControllerGrid>().startCoalQuantity = globalCoalQuantity;

        //coinQuantity = startCoinQuantity;

        //SetNextStation();
    }

    private void FixedUpdate()
    {
        

        RwTrainControllerGrid[] trains = FindObjectsByType<RwTrainControllerGrid>(FindObjectsSortMode.None);
        foreach (RwTrainControllerGrid train in trains)
        {

            if (train.isWagon == false)
            {
                train.FixedUpdateRemote();
            }
            
        }

        RwTrainControllerGrid[] wagons = FindObjectsByType<RwTrainControllerGrid>(FindObjectsSortMode.None);
        foreach (RwTrainControllerGrid wagon in wagons)
        {

            if (wagon.isWagon == true)
            {
                wagon.FixedUpdateRemote();
            }

        }


    }

    void Update()
    {

        FindFirstObjectByType<UICoin>().coinQuantity = coinQuantity;

       
    }

   
}
