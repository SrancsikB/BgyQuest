using UnityEngine;

public class UICurrentUser : MonoBehaviour
{

    void Update()
    {
        try
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = PlayerDataControl.Instance.playerName;
        }
        catch (System.Exception)
        {

            Debug.Log("SB: Player name finding issue");
        }
        

    }
}
