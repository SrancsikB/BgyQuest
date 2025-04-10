

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButtonAnimation : MonoBehaviour
{
    [SerializeField] float Offset = 1;

    public void MyPointerDown() {

        foreach (Transform child in transform)
        {
            Vector3 CurrentPos = child.transform.position;
            child.transform.position = CurrentPos - new Vector3(0, Offset, 0);
        }
    }

    public void MyPointerUp()
    {

        foreach (Transform child in transform)
        {
            Vector3 CurrentPos = child.transform.position;
            child.transform.position = CurrentPos - new Vector3(0, -Offset, 0);
        }
    }


    void OnMouseDown()
    {
        MyPointerDown();
    }

    void OnMouseUp()
    {
        MyPointerUp();
    }
}