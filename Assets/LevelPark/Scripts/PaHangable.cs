using UnityEngine;

public class PaHangable : MonoBehaviour
{
    enum Orientation
    {
        Left, Right
    }

    [SerializeField] Orientation orientation;


    private void OnValidate()
    {
        //if (orientation == Orientation.Left)
        //{
        //    transform.localScale = new Vector3(-1, 1, 1);
        //    transform.eulerAngles = new Vector3(0, 0, -175);
        //}
        //else
        //{
        //    transform.localScale = new Vector3(1, 1, 1);
        //    transform.eulerAngles = new Vector3(0, 0, -185);
        //}
    }

}
