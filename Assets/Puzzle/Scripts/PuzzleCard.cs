using UnityEngine;

public class PuzzleCard : MonoBehaviour
{
    public int order;
    public Vector3 correctPosition;
    public bool aquired = false;
    
    bool hovered = false;
    Vector3 startPos;
    Quaternion startRot;




    public void StoreStartPos()
    {
        startPos = transform.position;
        startRot = transform.rotation;

    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
