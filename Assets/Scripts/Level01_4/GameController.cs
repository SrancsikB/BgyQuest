using UnityEngine;

public class GameController : MonoBehaviour
{
    public int coinQuantity;

    //RV specific
    public float globalSwitchingTime;
    
    public int globalCoalQuantity;

    public int transportMinReward;
    public int transportMaxReward;
    public float transportTimeToDecreaseReward;

    public float coalHeapShowTime;
    public float coalHeapHideTime;
    public int coalHeapQuantity;


    private void Start()
    {
        //Setup start params
        RailSwitch[] rs = FindObjectsByType<RailSwitch>(FindObjectsSortMode.None);
        foreach (var railSwitch in rs)
        {
            railSwitch.switchingTime = globalSwitchingTime;
        }

        TrainControllerGrid[] trains = FindObjectsByType<TrainControllerGrid>(FindObjectsSortMode.None);
        trains[0].SelectTrain(); //Select 1st train by default
        foreach (TrainControllerGrid train in trains)
        {
            train.GetComponent<TrainControllerGrid>().startCoalQuantity = globalCoalQuantity;
            train.GetComponent<TrainControllerGrid>().minReward = transportMinReward;
            train.GetComponent<TrainControllerGrid>().maxReward = transportMaxReward;
            train.GetComponent<TrainControllerGrid>().maxTimeToDecreaseReward = transportTimeToDecreaseReward;
            train.GetComponent<TrainControllerGrid>().coalHeapShowTime = coalHeapShowTime;
            train.GetComponent<TrainControllerGrid>().coalHeapHideTime = coalHeapHideTime;
            train.GetComponent<TrainControllerGrid>().coalHeapQuantity = coalHeapQuantity;
        }


        try //try to get Player coins, 0 if NA
        {
            coinQuantity = PlayerDataControl.Instance.coins;
        }
        catch (System.Exception)
        {
            coinQuantity = 0;
            Debug.Log("SB: Coins cannot aquired from Player data");
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
