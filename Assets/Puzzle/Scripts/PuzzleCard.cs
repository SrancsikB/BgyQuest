using UnityEngine;

public class PuzzleCard : MonoBehaviour
{
    [SerializeField] Sprite backSideSprite;
    public Vector3 correctPosition;
    public bool aquired = false;
    bool hovered = false;
    Vector3 startPos;
    Quaternion startRot;

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!aquired)
        {
            GetComponent<SpriteRenderer>().sprite = backSideSprite;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (hovered && aquired)
        {
            Vector3 screenPos = Input.mousePosition;
            screenPos.z = Camera.main.nearClipPlane + 1;
            transform.position = Camera.main.ScreenToWorldPoint(screenPos);
        }

    }

    private void OnMouseDown()
    {

        if (aquired)
            transform.Rotate(0, 0, 90);


    }

    private void OnMouseDrag()
    {
        hovered = true;
    }

    private void OnMouseUp()
    {
        hovered = false;

        float gridPosX = Mathf.RoundToInt(transform.position.x);
        float gridPosY = Mathf.RoundToInt(transform.position.y);


        if (gridPosX >= -3 && gridPosX <= -1 && gridPosY >= -1 && gridPosY <= 1)
        {
            transform.position = new Vector3(gridPosX, gridPosY);
        }
        else
        {
            transform.position = startPos;
            transform.rotation = startRot;
        }


    }




}
