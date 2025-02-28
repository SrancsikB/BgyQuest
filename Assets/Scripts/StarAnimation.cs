using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    public int State;
    [SerializeField] float ScaleSpeed = 0.01f;
    [SerializeField] float AngleSpeed = 200;
    public Material MatOff;
    public Material MatActive;
    public Material MatDone;

    public GameObject PopUp;

    int ScaleDirection = 1;
    float MaxScale = 1.500f;
    float MinScale = 1.000f;

    float RotDir = 0;
    float RotMax = 5f;
    float RotMin = -5f;

    GameObject Star;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RotDir = RotMax;
        Star = transform.GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {

        switch (State)
        {
            case 2:
                Star.GetComponent<MeshRenderer>().material = MatActive;


                float ScaleFactor = ScaleSpeed * Time.deltaTime;
                Vector3 Scaler = new Vector3(1, 1, 0);
                transform.localScale += Scaler * ScaleFactor * ScaleDirection;
                if (transform.localScale.x > MaxScale)
                {
                    ScaleDirection = -1;
                    //transform.localScale = new Vector3(MaxScale, 0, MaxScale);
                }
                if (transform.localScale.x < MinScale)
                {
                    ScaleDirection = 1;
                    //transform.localScale = new Vector3(MinScale, 0, MinScale);
                }





                Quaternion TargetRot = Quaternion.Euler(new Vector3(0, 0, RotDir));
                float angle = AngleSpeed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRot, angle);

                if (transform.rotation == TargetRot)
                {
                    if (RotDir == RotMax)
                        RotDir = RotMin;
                    else
                        RotDir = RotMax;
                }

                break;
            case 3:
                Star.GetComponent<MeshRenderer>().material = MatDone;
                transform.rotation = Quaternion.Euler(Vector3.zero);
                transform.localScale = Vector3.one;

                break;
            default:
                Star.GetComponent<MeshRenderer>().material = MatOff;
                break;
        }



    }


    private void OnMouseDown()
    {
        if (State>=2)
        {
            Vector3 NewPos = new Vector3(transform.position.x + 0.7f, transform.position.y, PopUp.transform.position.z);
            if (PopUp.activeSelf == false) PopUp.SetActive(true);
            PopUp.transform.position = NewPos;
        }
        else
        {
            PopUp.SetActive(false);
        }
        
    }
}
