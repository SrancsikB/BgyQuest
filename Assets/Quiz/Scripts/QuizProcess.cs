using TMPro;
using UnityEngine;

public class QuizProcess : MonoBehaviour
{
    public MapFlag.GameGroup gg = MapFlag.GameGroup.Railway;
    public int quizLevel = 1;

    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI[] numericInput;

    QuizAnimReward qar;



    private void Start()
    {
        qar = FindFirstObjectByType<QuizAnimReward>(FindObjectsInactive.Include);
    }

    bool ProcessAnswer()
    {

        string answer = numericInput[3].text + numericInput[2].text + numericInput[1].text + numericInput[0].text;
        switch (gg)
        {
            case MapFlag.GameGroup.Railway:
                if (quizLevel == 1 && answer == "0028")
                {
                    TryPlayerDataStore(0);
                    return true;
                }
                else if (quizLevel == 2 && answer == "1896")
                {
                    TryPlayerDataStore(5);
                    return true;
                }
                else if (quizLevel == 3 && answer == "0008")
                {
                    TryPlayerDataStore(6);
                    return true;
                }
                break;
           
            default:
                break;
        }

        return false;
    }


    private void OnMouseDown()
    {
        if (ProcessAnswer() == true)
        {
            instructionText.color = Color.green;
            instructionText.text = "Correct answer!";
            qar.StartFlipAnim();

        }
        else
        {
            instructionText.color = Color.red;
            instructionText.text = "Incorrect answer!\r\nYou can retry it later...";

        }
        gameObject.SetActive(false);
    }


    private void TryPlayerDataStore(int index)
    {
        try
        {
            PlayerDataControl.Instance.SetRailwayPuzzleData(index, true);
        }
        catch (System.Exception)
        {
            Debug.Log("SB: Player data store failed...");
        }
    }

}
