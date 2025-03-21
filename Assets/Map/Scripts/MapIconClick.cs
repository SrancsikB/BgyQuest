using UnityEngine;
using UnityEngine.SceneManagement;

public class MapIconClick : MonoBehaviour
{
    public enum MapIcon
    {
        Quiz,Game,Puzzle
    }
    public MapFlag.GameGroup gameGroup;
    public MapIcon mapIcon;

    private void OnMouseDown()
    {
        switch (gameGroup)
        {
            case MapFlag.GameGroup.Railway:
                switch (mapIcon)
                {
                    case MapIcon.Quiz:
                        break;
                    case MapIcon.Game:
                        SceneManager.LoadScene("RailwayLevel");
                        break;
                    case MapIcon.Puzzle:
                        
                        SceneManager.LoadScene("Puzzle");
                        break;
                    default:
                        break;
                }
                break;
            case MapFlag.GameGroup.Soldier:
                break;
            case MapFlag.GameGroup.Hero:
                break;
            default:
                break;
        }
    }
}
