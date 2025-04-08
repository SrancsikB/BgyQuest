using UnityEngine;

public class UIStation : MonoBehaviour
{
    public string stationName;
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = stationName;
    }
}
