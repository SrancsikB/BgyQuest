using UnityEngine;

public class CircleCard : MonoBehaviour
{
    public float angularSpeed = 1f;
    public float circleRad = 1f;
    public float startAngleOffset = 1f;

    private Vector2 fixedPoint;
    private float currentAngle;

    void Start()
    {
        currentAngle = startAngleOffset * Mathf.PI;
        fixedPoint = transform.position;
    }

    void Update()
    {
        currentAngle += angularSpeed * Time.deltaTime;
        Vector2 offset = new Vector2(Mathf.Sin(currentAngle), Mathf.Cos(currentAngle)) * circleRad;
        transform.position = fixedPoint + offset;
    }


    private void OnMouseDown()
    {
        if (name== "CardTrain")
        {
            Debug.Log("Win");
        }
        else Debug.Log("Lose");
    }
}
