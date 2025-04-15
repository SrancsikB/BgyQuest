using UnityEngine;
using TMPro;


public class rwUpgradeController : MonoBehaviour
{
    public int coinQuantity;
    [SerializeField] PlayerDataControl.RailwayPogressData rwPD;
    [SerializeField] TextMeshProUGUI rwTrainSpeedUpgradeInfo;
    [SerializeField] TextMeshProUGUI rwTrainSpeedUpgradeLevel;
    [SerializeField] TextMeshProUGUI rwTrainSpeedUpgradeCost;

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


        rwTrainSpeedUpgradeInfo.text = "Train speed \u000a Buy this upgrade to increase the speed of the train with 50% to get more bonus with faster jurney time!";
        rwTrainSpeedUpgradeLevel.text = rwPD.rwTrainSpeed.ToString();

        switch (rwPD.rwTrainSpeed)
        {
            case 1:
                rwTrainSpeedUpgradeCost.text = 1000.ToString();
                break;
            case 2:
                rwTrainSpeedUpgradeCost.text = 2500.ToString();
                break;
            case 3:
                rwTrainSpeedUpgradeCost.text = 5000.ToString();
                break;
            case 4:
                rwTrainSpeedUpgradeCost.text = 8000.ToString();
                break;
            default:
                break;
        }

    }

    void Update()
    {

        FindFirstObjectByType<UICoin>().coinQuantity = coinQuantity;

       
    }
}
