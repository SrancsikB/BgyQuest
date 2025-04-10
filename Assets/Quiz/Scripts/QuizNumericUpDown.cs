using UnityEngine;
using TMPro;

public class QuizNumericUpDown : MonoBehaviour
{
    [SerializeField] int modifier;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite pushedSprite;

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sprite = pushedSprite;
        int currValue = int.Parse(text.text);
        currValue += modifier;
        if (currValue > 9)
            currValue = 0;
        else if (currValue < 0)
            currValue = 9;
        text.text = currValue.ToString();
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().sprite = normalSprite;
    }
}
