using UnityEngine;

public class UIStation : MonoBehaviour
{
    public string stationName;
    public void UpdateValue(string value)
    {
        stationName = value;
        GetComponent<TMPro.TextMeshProUGUI>().text = stationName;
    }

    public void ClearValue()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "-----";
    }
}
