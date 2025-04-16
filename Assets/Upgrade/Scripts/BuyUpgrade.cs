using UnityEngine;
using TMPro;

public class BuyUpgrade : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] GameObject buyButton;

    bool upgradeable = false;
    void Update()
    {

        if (FindFirstObjectByType<UICoin>().coinQuantity > int.Parse(cost.text))
        {
            upgradeable = true;
            buyButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
        else
        {
            upgradeable = false;
            buyButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        }


    }
    private void OnMouseDown()
    {
        if (upgradeable)
        {
            RwUpgradeController gc = FindFirstObjectByType<RwUpgradeController>();
            gc.coinQuantity -= int.Parse(cost.text);
            gc.rwPD.rwTrainSpeed += 1;
            
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
