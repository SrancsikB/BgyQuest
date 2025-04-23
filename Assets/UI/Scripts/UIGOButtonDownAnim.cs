using UnityEngine;

public class UIGOButtonDownAnim : MonoBehaviour
{
    [SerializeField] float textOffset;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite pushedSprite;

    private void OnMouseDown()
    {
        if (enabled)
        {
            GetComponent<SpriteRenderer>().sprite = pushedSprite;
            foreach (Transform child in transform)
            {
                Vector3 CurrentPos = child.transform.position;
                child.transform.position = CurrentPos - new Vector3(0, textOffset, 0);
            }
        }

    }

    private void OnMouseUp()
    {
        if (enabled)
        {
            GetComponent<SpriteRenderer>().sprite = normalSprite;
            foreach (Transform child in transform)
            {
                Vector3 CurrentPos = child.transform.position;
                child.transform.position = CurrentPos - new Vector3(0, -textOffset, 0);
            }
        }

    }
}
