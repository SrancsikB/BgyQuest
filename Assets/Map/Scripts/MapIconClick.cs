using TMPro;
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
    [SerializeField] bool lockQuiz;
    [SerializeField] GameObject[] quizButtons;


    private void Start()
    {
        try
        {
            lockQuiz = PlayerDataControl.Instance.lockQuiz;

        }
        catch (System.Exception)
        {
            Debug.Log("SB: Cannot load quiz lock, use field value");
        }


        foreach (GameObject quizButton in quizButtons)
        {
            if (lockQuiz == true)
            {
                quizButton.GetComponent<UIGOButtonDownAnim>().enabled = false;
                quizButton.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            else
            {
                quizButton.GetComponent<UIGOButtonDownAnim>().enabled = true;
                quizButton.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
            }
        }
    }

    private void OnMouseDown()
    {
        switch (gameGroup)
        {
            case MapFlag.GameGroup.Railway:
                switch (mapIcon)
                {
                    case MapIcon.Quiz:
                        if (lockQuiz == false)
                            SceneManager.LoadScene("Quiz");
                        break;
                    case MapIcon.Game:
                        SceneManager.LoadScene("RailwayLevelSelect");
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
            case MapFlag.GameGroup.Park:
                switch (mapIcon)
                {
                    case MapIcon.Quiz:
                        if (lockQuiz == false)
                        { }
                            //SceneManager.LoadScene("Quiz");
                            
                        break;
                    case MapIcon.Game:
                        SceneManager.LoadScene("ParkLevelSelect");
                        break;
                    case MapIcon.Puzzle:

                        //SceneManager.LoadScene("Puzzle");
                        break;
                    case MapIcon.Upgrade:
                        //SceneManager.LoadScene("RailwayUpgrade");
                        break;

                    default:
                        break;
                }
                break;

            case MapFlag.GameGroup.Soldier:
                switch (mapIcon)
                {
                    case MapIcon.Quiz:
                        if (lockQuiz == false)
                        { }
                        //SceneManager.LoadScene("Quiz");

                        break;
                    case MapIcon.Game:
                        SceneManager.LoadScene("SoldierLevel");
                        break;
                    case MapIcon.Puzzle:

                        //SceneManager.LoadScene("Puzzle");
                        break;
                    case MapIcon.Upgrade:
                        //SceneManager.LoadScene("RailwayUpgrade");
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
