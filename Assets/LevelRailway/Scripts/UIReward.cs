using UnityEngine;

public class UIReward : MonoBehaviour
{
    public int reward;
    public void UpdateValue(int value)
    {
        reward = value;
        GetComponent<TMPro.TextMeshProUGUI>().text = reward.ToString();
    }
    public void ClearValue()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "-----";
    }
}
