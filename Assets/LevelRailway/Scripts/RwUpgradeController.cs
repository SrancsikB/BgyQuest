using TMPro;
using UnityEngine;


public class RwUpgradeController : MonoBehaviour
{
    public int coinQuantity;
    public PlayerDataControl.RailwayPogressData rwPD;

    [SerializeField] TextMeshProUGUI rwMapLevelUpgradeInfo;
    [SerializeField] TextMeshProUGUI rwMapLevelUpgradeLevel;
    [SerializeField] TextMeshProUGUI rwMapLevelUpgradeCost;
    [SerializeField] GameObject rwMapLevelGOCost;
    [SerializeField] GameObject rwMapLevelGOBuy;

    [SerializeField] TextMeshProUGUI rwTrainSpeedUpgradeInfo;
    [SerializeField] TextMeshProUGUI rwTrainSpeedUpgradeLevel;
    [SerializeField] TextMeshProUGUI rwTrainSpeedUpgradeCost;
    [SerializeField] GameObject rwTrainSpeedGOCost;
    [SerializeField] GameObject rwTrainSpeedGOBuy;

    [SerializeField] TextMeshProUGUI rwWagonLevelUpgradeInfo;
    [SerializeField] TextMeshProUGUI rwWagonLevelUpgradeLevel;
    [SerializeField] TextMeshProUGUI rwWagonLevelUpgradeCost;
    [SerializeField] GameObject rwWagonLevelGOCost;
    [SerializeField] GameObject rwWagonLevelGOBuy;

    [SerializeField] TextMeshProUGUI rwSwitchingTimeUpgradeInfo;
    [SerializeField] TextMeshProUGUI rwSwitchingTimeUpgradeLevel;
    [SerializeField] TextMeshProUGUI rwSwitchingTimeUpgradeCost;
    [SerializeField] GameObject rwSwitchingTimeGOCost;
    [SerializeField] GameObject rwSwitchingTimeGOBuy;

    [SerializeField] TextMeshProUGUI rwCoalHeapUpgradeInfo;
    [SerializeField] TextMeshProUGUI rwCoalHeapUpgradeLevel;
    [SerializeField] TextMeshProUGUI rwCoalHeapUpgradeCost;
    [SerializeField] GameObject rwCoalHeapGOCost;
    [SerializeField] GameObject rwCoalHeapGOBuy;

    [SerializeField] TextMeshProUGUI rwCoinHeapUpgradeInfo;
    [SerializeField] TextMeshProUGUI rwCoinHeapUpgradeLevel;
    [SerializeField] TextMeshProUGUI rwCoinHeapUpgradeCost;
    [SerializeField] GameObject rwCoinHeapGOCost;
    [SerializeField] GameObject rwCoinHeapGOBuy;

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
            //coinQuantity = 0;
            Debug.Log("SB: Player data cannot be aquired, using public progress data");
        }

        rwMapLevelUpgradeInfo.text = "Maps \u000a Buy maps for new territories, even with more trains!";
        rwTrainSpeedUpgradeInfo.text = "Train speed \u000a Buy this upgrade to increase the speed of the train to get more bonus with faster transport time!";
        rwWagonLevelUpgradeInfo.text = "Wagons \u000a Buy wagons for more transport bonus and more coal capacity!";
        rwSwitchingTimeUpgradeInfo.text = "Switches \u000a Buy this upgrade to reduce the switching time!";
        rwCoalHeapUpgradeInfo.text = "Coals \u000a Buy this upgrade to appear coals more offen with higher capacity!";
        rwCoinHeapUpgradeInfo.text = "Coins \u000a Buy this upgrade to appear coins more offen with higher value!";
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            //Using ray just to try it. It helps to do all scene interaction within this script
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit;
            hit = Physics2D.GetRayIntersection(ray);

            if (hit)
            {
                if (hit.transform.gameObject == rwMapLevelGOBuy)
                {
                    coinQuantity -= int.Parse(rwMapLevelUpgradeCost.text);
                    rwPD.rwMapLevel += 1;
                }
                else if (hit.transform.gameObject == rwTrainSpeedGOBuy)
                {
                    coinQuantity -= int.Parse(rwTrainSpeedUpgradeCost.text);
                    rwPD.rwTrainSpeed += 1;
                }
                else if (hit.transform.gameObject == rwWagonLevelGOBuy)
                {
                    coinQuantity -= int.Parse(rwWagonLevelUpgradeCost.text);
                    rwPD.rwWagonLevel += 1;
                }
                else if (hit.transform.gameObject == rwSwitchingTimeGOBuy)
                {
                    coinQuantity -= int.Parse(rwSwitchingTimeUpgradeCost.text);
                    rwPD.rwSwitchingTime += 1;
                }
                else if (hit.transform.gameObject == rwCoalHeapGOBuy)
                {
                    coinQuantity -= int.Parse(rwCoalHeapUpgradeCost.text);
                    rwPD.rwCoalHeapLevel += 1;
                }
                else if (hit.transform.gameObject == rwCoinHeapGOBuy)
                {
                    coinQuantity -= int.Parse(rwCoinHeapUpgradeCost.text);
                    rwPD.rwBonusCoinLevel += 1;
                }
                else if (hit.transform.gameObject.name == "ButtonGimme1000")
                {
                    coinQuantity += 1000;
                }


                try
                {
                    PlayerDataControl.Instance.SaveCoinData(coinQuantity);
                    PlayerDataControl.Instance.SetRailwayProgressData(rwPD);
                }
                catch (System.Exception)
                {
                    Debug.Log("Coin save failed");
                }
            }
        }



        FindFirstObjectByType<UICoin>().coinQuantity = coinQuantity;

        CheckUpgradeLevel(rwPD.rwTrainSpeed, rwTrainSpeedUpgradeCost, rwTrainSpeedUpgradeLevel, rwTrainSpeedGOCost, rwTrainSpeedGOBuy, 
            new int[] { 1000, 2500, 5000 });
        CheckUpgradeLevel(rwPD.rwSwitchingTime, rwSwitchingTimeUpgradeCost, rwSwitchingTimeUpgradeLevel, rwSwitchingTimeGOCost, rwSwitchingTimeGOBuy,
            new int[] { 500, 1000, 2500 });
        CheckUpgradeLevel(rwPD.rwWagonLevel, rwWagonLevelUpgradeCost, rwWagonLevelUpgradeLevel, rwWagonLevelGOCost, rwWagonLevelGOBuy,
            new int[] { 1000, 2500, 5000 });
        CheckUpgradeLevel(rwPD.rwMapLevel, rwMapLevelUpgradeCost, rwMapLevelUpgradeLevel, rwMapLevelGOCost, rwMapLevelGOBuy,
            new int[] { 2000, 5000, 10000 });
        CheckUpgradeLevel(rwPD.rwCoalHeapLevel, rwCoalHeapUpgradeCost, rwCoalHeapUpgradeLevel, rwCoalHeapGOCost, rwCoalHeapGOBuy,
            new int[] { 200, 500, 800 });
        CheckUpgradeLevel(rwPD.rwBonusCoinLevel, rwCoinHeapUpgradeCost, rwCoinHeapUpgradeLevel, rwCoinHeapGOCost, rwCoinHeapGOBuy,
            new int[] { 200, 500, 800 });



        /*
        int cost;
        bool hasCoinsToBuy;




        //MapLevel



        cost = 0;
        hasCoinsToBuy = false;
        switch (rwPD.rwTrainSpeed)
        {
            case 1:
                cost = 1000;
                if (coinQuantity >= cost) hasCoinsToBuy = true;
                rwTrainSpeedUpgradeCost.text = cost.ToString();
                break;
            case 2:
                cost = 2500;
                if (coinQuantity >= cost) hasCoinsToBuy = true;
                rwTrainSpeedUpgradeCost.text = cost.ToString();
                break;
            case 3:
                cost = 5000;
                if (coinQuantity >= cost) hasCoinsToBuy = true;
                rwTrainSpeedUpgradeCost.text = cost.ToString();
                break;
            default:
                rwTrainSpeedUpgradeCost.text = cost.ToString();
                break;
        }


        if (rwPD.rwTrainSpeed < 4)
        {
            rwTrainSpeedUpgradeLevel.text = rwPD.rwTrainSpeed.ToString();
            rwTrainSpeedGOCost.SetActive(true);
            rwTrainSpeedGOBuy.SetActive(true);
            if (hasCoinsToBuy)
            { 
                rwTrainSpeedGOBuy.GetComponent<Collider2D>().enabled = true;
                rwTrainSpeedGOBuy.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
            }
            else
            {
                rwTrainSpeedGOBuy.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
                rwTrainSpeedGOBuy.GetComponent<Collider2D>().enabled = false;
            }
        }
        else
        {
            rwTrainSpeedUpgradeLevel.text = "Max";
            rwTrainSpeedGOCost.SetActive(false);
            rwTrainSpeedGOBuy.SetActive(false);

        }*/


    }




    private void CheckUpgradeLevel(int level, TextMeshProUGUI costUI, TextMeshProUGUI levelUI, GameObject costGO, GameObject buyGO, int[] costs)
    {
        
        int cost = 0;
        bool hasCoinsToBuy = false;

        switch (level)
        {
            case 1:
                cost = costs[0];
                if (coinQuantity >= cost) hasCoinsToBuy = true;
                costUI.text = cost.ToString();
                break;
            case 2:
                cost = costs[1];
                if (coinQuantity >= cost) hasCoinsToBuy = true;
                costUI.text = cost.ToString();
                break;
            case 3:
                cost = costs[2];
                if (coinQuantity >= cost) hasCoinsToBuy = true;
                costUI.text = cost.ToString();
                break;
            default:
                costUI.text = cost.ToString();
                break;
        }


        if (level < 4)
        {
            levelUI.text = level.ToString();
            costGO.SetActive(true);
            buyGO.SetActive(true);
            if (hasCoinsToBuy)
            {
                buyGO.GetComponent<Collider2D>().enabled = true;
                buyGO.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
            }
            else
            {
                buyGO.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
                buyGO.GetComponent<Collider2D>().enabled = false;
            }
        }
        else
        {
            levelUI.text = "Max";
            costGO.SetActive(false);
            buyGO.SetActive(false);

        }
    }

}
