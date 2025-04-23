using UnityEngine;

public class UICoal : MonoBehaviour
{
    public float coalQuantity;
    public void UpdateValue(float value)
    {
        coalQuantity = value;
        GetComponent<TMPro.TextMeshProUGUI>().text = coalQuantity.ToString();
    }
    public void ClearValue()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "-----";
    }
}
