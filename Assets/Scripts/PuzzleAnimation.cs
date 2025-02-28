using UnityEngine;

public class PuzzleAnimation : MonoBehaviour
{
    public int State;
    [SerializeField] float MoveSpeed = 0.01f;
    public Material MatOff;
    public Material MatActive;
    public Material MatDone;
    int Direction1 = -1;
    int Direction2 = 1;
    float MaxMove = 0.0f;
    float MinMove = -0.10f;

    

    GameObject Puzzle1;
    GameObject Puzzle2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Puzzle1 = transform.GetChild(0).gameObject;
        Puzzle2 = transform.GetChild(1).gameObject;
        


    }

    // Update is called once per frame
    void Update()
    {

        switch (State)
        {
            case 2:
                Puzzle1.GetComponent<MeshRenderer>().material = MatActive;
                Puzzle2.GetComponent<MeshRenderer>().material = MatActive;
                break;
            case 3:
                Puzzle1.GetComponent<MeshRenderer>().material = MatDone;
                Puzzle2.GetComponent<MeshRenderer>().material = MatDone;
                break;
            default:
                Puzzle1.GetComponent<MeshRenderer>().material = MatOff;
                Puzzle2.GetComponent<MeshRenderer>().material = MatOff;
                break;
        }


        float MoveFactor = MoveSpeed * Time.deltaTime;
        Vector3 Scaler = new Vector3(0, 1, 0);
        Puzzle1.transform.localPosition += Scaler * MoveFactor * Direction1;
        if (Puzzle1.transform.localPosition.y > MaxMove)
        {
            Direction1 = -1;
            
        }
        if (Puzzle1.transform.localPosition.y < MinMove)
        {
            Direction1 = 1;
            
        }


        Puzzle2.transform.localPosition += Scaler * MoveFactor * Direction2;
        if (Puzzle2.transform.localPosition.y > MaxMove)
        {
            Direction2 = -1;

        }
        if (Puzzle2.transform.localPosition.y < MinMove)
        {
            Direction2 = 1;

        }

    }
}
