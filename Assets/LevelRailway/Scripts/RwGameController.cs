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
        transportMinReward = 25 + rwPD.rwWagonLevel * 25;
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
            train.GetComponent<RwTrainControllerGrid>().startCoalQuantity = globalCoalQuantity;
            train.GetComponent<RwTrainControllerGrid>().minReward = transportMinReward;
            train.GetComponent<RwTrainControllerGrid>().maxReward = transportMaxReward;
            train.GetComponent<RwTrainControllerGrid>().maxTimeToDecreaseReward = transportTimeToDecreaseReward;
            train.GetComponent<RwTrainControllerGrid>().coalHeapShowTime = coalHeapShowTime;
            train.GetComponent<RwTrainControllerGrid>().coalHeapHideTime = coalHeapHideTime;
            train.GetComponent<RwTrainControllerGrid>().coalHeapQuantity = coalHeapQuantity;
            train.GetComponent<RwTrainControllerGrid>().coinHeapShowTime = coinHeapShowTime;
            train.GetComponent<RwTrainControllerGrid>().coinHeapHideTime = coinHeapHideTime;
            train.GetComponent<RwTrainControllerGrid>().coinHeapQuantity = coinHeapQuantity;
        }





        //wagon.GetComponent<TrainControllerGrid>().startCoalQuantity = globalCoalQuantity;

        //coinQuantity = startCoinQuantity;

        //SetNextStation();
    }

    void Update()
    {

        FindFirstObjectByType<UICoin>().coinQuantity = coinQuantity;

        ////Release movement
        //if (train.GetComponent<TrainControllerGrid>().stopMovement == true && Input.GetKeyDown(KeyCode.Space))
        //{

        //    SetNextStation();
        //    train.GetComponent<TrainControllerGrid>().stopMovement = false;
        //    wagon.GetComponent<TrainControllerGrid>().stopMovement = false;
        //}


        //foreach (Transform goStation in stations.transform)
        //{
        //    if (goStation.GetComponent<StationController>().stationName == train.GetComponent<TrainControllerGrid>().targetStation)
        //    {
        //        goStation.GetChild(0).gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        goStation.GetChild(0).gameObject.SetActive(false);
        //    }
        //}
    }

    //public void SetNextStation()
    //{
    //    StationController.StationNames newRandomStation = train.GetComponent<TrainControllerGrid>().targetStation;

    //    while (newRandomStation== train.GetComponent<TrainControllerGrid>().targetStation) //Avoid same seed
    //    {
    //        int index = Random.Range(0, stations.transform.childCount);
    //        newRandomStation = stations.transform.GetChild(index).GetComponent<StationController>().stationName;

    //    }
    //    train.GetComponent<TrainControllerGrid>().targetStation = newRandomStation;
    //    wagon.GetComponent<TrainControllerGrid>().targetStation = newRandomStation;
    //}
}
