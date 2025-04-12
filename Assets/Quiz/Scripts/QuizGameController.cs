using UnityEngine;
using TMPro;

public class QuizGameController : MonoBehaviour
{
    public int coinQuantity;
    [SerializeField] MapFlag.GameGroup gg;
    [SerializeField] int quizLevel;
    [SerializeField] TextMeshProUGUI quizMsg;
    [SerializeField] Sprite[] rvRewardSprites;

    QuizProcess qp;
    QuizAnimReward qar;


    void Start()
    {
        qp = FindFirstObjectByType<QuizProcess>();
        qp.quizLevel = quizLevel;
        qar = FindFirstObjectByType<QuizAnimReward>(FindObjectsInactive.Include);
        
        switch (gg)
        {
            case MapFlag.GameGroup.Railway:
                switch (quizLevel)
                {
                    case 1:
                        quizMsg.text = "Find the oldest train in the train station. You will find a plate with numbers on its side. What are the sum of these numbers?";
                        qar.rewardSprite = rvRewardSprites[0];
                        break;
                    case 2:
                        quizMsg.text = "Walk around the train station! Find out which year the Aszod-Balassagyarmat-Losonc railway has been opened?";
                        qar.rewardSprite = rvRewardSprites[1];
                        break;
                    case 3:
                        quizMsg.text = "Pass the bridge above the railways! How many pair of rails do you pass?";
                        qar.rewardSprite = rvRewardSprites[2];
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
