using UnityEngine;

public class FlipCard : MonoBehaviour
{
    [SerializeField] float AngleSpeed=200;
    public bool RotationActive = true;
    float RotDir = 180f;
        
    // Update is called once per frame
    void Update()
    {
        if (RotationActive)
        {
            Quaternion TargetRot= Quaternion.Euler(new Vector3 (0, RotDir, 0));
            float angle = AngleSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRot, angle);

            if (transform.rotation==TargetRot)
            {
                RotationActive = false;
                if (RotDir == 180f)
                    RotDir = 0;
                else
                    RotDir = 180f;
            }
        }
    }

    private void OnMouseDown()
    {
        RotationActive = true;
    }
}
