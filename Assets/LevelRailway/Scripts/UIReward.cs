using UnityEngine;

public class UIReward : MonoBehaviour
{
    public int reward;
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = reward.ToString();
    }
}
