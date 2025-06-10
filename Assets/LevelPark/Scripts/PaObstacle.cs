using UnityEngine;

public class PaObstacle : MonoBehaviour
{
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    public void resetPos()
    {
        transform.position = startPos;
    }
}
