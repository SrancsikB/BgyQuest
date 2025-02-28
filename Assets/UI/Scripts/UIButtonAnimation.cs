

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButtonAnimation : MonoBehaviour
{
    [SerializeField] int Offset = 20;

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

}