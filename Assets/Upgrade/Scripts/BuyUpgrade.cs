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

        }
    }
    }
