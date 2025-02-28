using UnityEngine;

public class RotTry : MonoBehaviour
{

    [SerializeField] float angularSpeed=10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    bool startRot;

    // Update is called once per frame
    void Update()
    {
        Vector3 rotPoint = new Vector3(-0.5f, 1.5f, 0);
        Vector3 targetPoint= new Vector3(-0.5f, 2f, 0);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            startRot = true;
            
        }
        if (startRot)
        {
            transform.RotateAround(rotPoint, new Vector3(0, 0, 1), Time.deltaTime * angularSpeed);
            if (Vector3.Distance(transform.position,targetPoint)<=0.05f)
            {
                transform.position = targetPoint;
                transform.rotation=Quaternion.Euler(0, 0, 90);
                startRot = false;

            }
        }
        
    }
}
