using UnityEngine;

public class UISwicthingTime : MonoBehaviour
{
    public float switchingTime;
    public float elapsedTime;
    void Update()
    {
        if (elapsedTime != 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<TMPro.TextMeshProUGUI>().text = System.Math.Round(switchingTime-elapsedTime,1).ToString("0.0");
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }
        
    }
}
