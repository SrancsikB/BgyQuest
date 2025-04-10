using UnityEngine;

public class QuizGameController : MonoBehaviour
{
    public int coinQuantity;
    [SerializeField] MapFlag.GameGroup gg;
    [SerializeField] int quizLevel;

    void Start()
    {
       
        switch (gg)
        {
            case MapFlag.GameGroup.Railway:
                switch (quizLevel)
                {
                    case 1:

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
