using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    public KeyCode switchButton;

    GameObject Canvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            Canvas = transform.GetChild(0).gameObject;
            Canvas.GetComponentInChildren<UISwitchingNum>().switchingNum = switchButton.ToString();
        }
        catch (System.Exception)
        {


        }
    }

    private void OnMouseDown()
    {
        RailSwitch[] switches = FindObjectsByType<RailSwitch>(FindObjectsSortMode.None);

        foreach (RailSwitch item in switches)
        {
            if (switchButton == item.switchButton)
            {
                item.triggerSwitch = true;
            }
        }
    }
    private void OnDrawGizmos()
    {

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = 10 - (int)transform.position.y;
        int X = Mathf.RoundToInt(transform.position.x);
        int Y = Mathf.RoundToInt(transform.position.y);
        transform.position = new Vector3(X, Y, 0);
    }
}
