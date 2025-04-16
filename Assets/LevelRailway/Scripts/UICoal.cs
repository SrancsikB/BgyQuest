using UnityEngine;

public class UICoal : MonoBehaviour
{
    public float coalQuantity;
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = coalQuantity.ToString();
    }
}
