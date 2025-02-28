using UnityEngine;
using TMPro;

public class UISwitchingNum : MonoBehaviour
{
    public string switchingNum;
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = switchingNum;
    }
}
