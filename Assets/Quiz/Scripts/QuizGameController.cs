using TMPro;
using UnityEngine;

public class QuizGameController : MonoBehaviour
{
    public int coinQuantity;
    [SerializeField] MapFlag.GameGroup gg;
    [SerializeField] int quizLevel;
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
                        //PlayerDataControl.Instance.SetRailwayPuzzleData(0, true);
                        break;
                    case 2:
                        quizMsg.text = "Walk around the train station! Find out which year the Aszod-Balassagyarmat-Losonc railway has been opened?";
                        qar.rewardSprite = rwRewardSprites[1];
                        //PlayerDataControl.Instance.SetRailwayPuzzleData(5, true);
                        break;
                    case 3:
                        quizMsg.text = "Pass the bridge above the railways! How many pair of rails do you pass?";
                        qar.rewardSprite = rwRewardSprites[2];
                        //PlayerDataControl.Instance.SetRailwayPuzzleData(6, true);
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;
        }


        qp = FindFirstObjectByType<QuizProcess>();
        qp.quizLevel = quizLevel;
        

    }

}
