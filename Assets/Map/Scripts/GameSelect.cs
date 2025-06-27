using UnityEngine;

public class GameSelect : MonoBehaviour
{


    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(MapFlag.GameGroup gg)
    {
        switch (gg)
        {
            case MapFlag.GameGroup.Railway:
                transform.position = new Vector3(5, -4, 0);
                break;
            case MapFlag.GameGroup.Park:
                transform.position = new Vector3(2, 0, 0);
                break;
            case MapFlag.GameGroup.Soldier:
                transform.position = new Vector3(3, -1, 0);
                break;
            default:
                break;
        }
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        
    }

}
