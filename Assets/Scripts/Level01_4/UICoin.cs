using UnityEngine;

public class UICoin : MonoBehaviour
{
    public float coinQuantity;
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = coinQuantity.ToString();
    }
}
