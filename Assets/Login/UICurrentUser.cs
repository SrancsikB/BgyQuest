using UnityEngine;

public class UICurrentUser : MonoBehaviour
{

    void Update()
    {

        GetComponent<TMPro.TextMeshProUGUI>().text = PlayerDataControl.Instance.playerName;

    }
}
