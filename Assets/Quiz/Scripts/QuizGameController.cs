using TMPro;
using UnityEngine;

public class QuizGameController : MonoBehaviour
{
    public int coinQuantity;
    [SerializeField] MapFlag.GameGroup gg;
    [SerializeField] int quizLevel;
    [SerializeField] GameObject buttonOK;
    [SerializeField] TextMeshProUGUI quizMsg;
    [SerializeField] Sprite[] rwRewardSprites;


    QuizProcess qp;
    QuizAnimReward qar;


    void Awake()
    {

        qar = FindFirstObjectByType<QuizAnimReward>(FindObjectsInactive.Include);


        switch (gg)
        {
            case MapFlag.GameGroup.Railway:
                try //try to get Player data
                {
                    PlayerDataControl pDC = PlayerDataControl.Instance;
                    coinQuantity = pDC.coins;


                    if (pDC.GetRailwayPuzzleData()[0] == false)
                        quizLevel = 1;
                    else if (pDC.GetRailwayPuzzleData()[5] == false)
                        quizLevel = 2;
                    else if (pDC.GetRailwayPuzzleData()[6] == false)
                        quizLevel = 3;
                    else
                        quizLevel = 0;
                }
                catch (System.Exception)
                {
                    Debug.Log("SB: Player data cannot be aquired, using public progress data");
                }
                switch (quizLevel)
                {
                    case 1:
                        quizMsg.text = "Find the oldest train in the train station. You will find a plate with numbers on its side. What are the sum of these numbers?";
                        qar.rewardSprite = rwRewardSprites[0];
                        break;
                    case 2:
                        quizMsg.text = "Walk around the train station! Find out which year the Aszod-Balassagyarmat-Losonc railway has been opened?";
                        qar.rewardSprite = rwRewardSprites[1];
                        break;
                    case 3:
                        quizMsg.text = "Pass the bridge above the railways! How many pair of rails do you pass?";
                        qar.rewardSprite = rwRewardSprites[2];
                        break;
                    default:
                        quizMsg.text = "Congratulations! You have asnwered all quiz questions! Puzzle rewards granted!";
                        quizMsg.color = new Color(0, 0.5f, 0, 1);//Green
                        buttonOK.SetActive(false);
                        break;
                }
                break;

            default:
                break;
        }


        qp = FindFirstObjectByType<QuizProcess>();
        qp.quizLevel = quizLevel;


    }

    private void Update()
    {
        FindFirstObjectByType<UICoin>().coinQuantity = coinQuantity;
    }

}
