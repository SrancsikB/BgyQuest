using UnityEngine;
using UnityEngine.SceneManagement;

public class MapIconClick : MonoBehaviour
{
    public enum MapIcon
    {
        Quiz, Game, Puzzle, Upgrade
    }
    public MapFlag.GameGroup gameGroup;
    public MapIcon mapIcon;
    [SerializeField] int level = 1;

    private void OnMouseDown()
    {
        switch (gameGroup)
        {
            case MapFlag.GameGroup.Railway:
                switch (mapIcon)
                {
                    case MapIcon.Quiz:
                        SceneManager.LoadScene("Quiz");
                        break;
                    case MapIcon.Game:
                        try
                        {
                            PlayerDataControl.RailwayPogressData rwPD;
                            rwPD = PlayerDataControl.Instance.GetRailwayProgressData();
                            level = rwPD.rwMapLevel;
                        }
                        catch (System.Exception)
                        {
                            Debug.Log("SB: Cannot load map level");
                        }
                        SceneManager.LoadScene("RailwayLevel" + level.ToString());
                        break;
                    case MapIcon.Puzzle:

                        SceneManager.LoadScene("Puzzle");
                        break;
                    case MapIcon.Upgrade:
                        SceneManager.LoadScene("RailwayUpgrade");
                        break;

                    default:
                        break;
                }
                break;

            default:
                break;
        }
    }
}
