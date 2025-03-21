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
            case MapFlag.GameGroup.Soldier:
                break;
            case MapFlag.GameGroup.Hero:
                break;
            default:
                break;
        }

        gameObject.SetActive(true);
    }
    
}
