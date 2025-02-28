using UnityEngine;

public class GenerateMap : MonoBehaviour
{



    public GameObject GO_TurnUL;
    public GameObject GO_TurnUR;
    public GameObject GO_TurnDR;
    public GameObject GO_TurnDL;
    public GameObject GO_LineUD;
    public GameObject GO_LineLR;
    public GameObject GO_CrossUDR;
    public GameObject GO_CrossLRU;
    public GameObject GO_Station;
    public GameObject GO_Train;

    private float XOffset = 1.15f;
    private float YOffset = 1.0f;

    private bool Switch1 = false;
    private bool Switch2 = false;
    private bool Switch3 = false;
    private bool Switch4 = false;
    private bool Switch5 = false;
    private bool Switch6 = false;

    private GameObject Switch1A;
    private GameObject Switch1B;
    private GameObject Switch2A;
    private GameObject Switch2B;
    private GameObject Switch3A;
    private GameObject Switch3B;
    private GameObject Switch4A;
    private GameObject Switch4B;
    private GameObject Switch5A;
    private GameObject Switch5B;
    private GameObject Switch6A;
    private GameObject Switch6B;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        GenerateTileMap();
        PlaceStations();
    }


    private void GenerateTileMap()
    {
        TrainController trainController = GO_Train.GetComponent<TrainController>();
        GameObject GO_Temp;
        for (int y = 0; y < 12; y++)
        {
            for (int x = 0; x < 16; x++)
            {
                if (x == 0 && y == 0) CreateMapTile(x, y, GO_TurnUR);
                if (x == 0 && y >= 1 && y < 11) CreateMapTile(x, y, GO_LineUD);
                if (x == 0 && y == 11) CreateMapTile(x, y, GO_TurnDR);

                if (x == 1 && y == 0) CreateMapTile(x, y, GO_LineLR);
                if (x == 1 && y == 11) CreateMapTile(x, y, GO_LineLR);

                if (x == 2 && y == 0) CreateMapTile(x, y, GO_TurnUL);
                if (x == 2 && y == 1) CreateMapTile(x, y, GO_CrossUDR);
                if (x == 2 && y == 2) CreateMapTile(x, y, GO_LineUD);
                //if (x == 2 && y >= 3 && y < 9) CreateMapTile(x, y, GO_LineUD);
                if (x == 2 && y == 3) Switch4A = CreateMapTile(x, y, GO_LineUD, "Switch4A");
                if (x == 2 && y == 3) Switch4B = CreateMapTile(x, y, GO_TurnUR, "Switch4B");
                if (x == 2 && y == 4) CreateMapTile(x, y, GO_LineUD);
                if (x == 2 && y == 5) CreateMapTile(x, y, GO_LineUD);
                if (x == 2 && y == 6) Switch3A = CreateMapTile(x, y, GO_LineUD, "Switch3A");
                if (x == 2 && y == 6) Switch3B = CreateMapTile(x, y, GO_TurnUR, "Switch3B");
                if (x == 2 && y == 7) CreateMapTile(x, y, GO_LineUD);
                if (x == 2 && y == 8) CreateMapTile(x, y, GO_LineUD);
                if (x == 2 && y == 9) CreateMapTile(x, y, GO_TurnDR);
                if (x == 2 && y == 11) CreateMapTile(x, y, GO_LineLR);

                if (x == 3 && y == 1) CreateMapTile(x, y, GO_LineLR);
                if (x == 3 && y == 3) CreateMapTile(x, y, GO_LineLR);
                if (x == 3 && y == 6) CreateMapTile(x, y, GO_LineLR);
                if (x == 3 && y == 9) CreateMapTile(x, y, GO_LineLR);
                if (x == 3 && y == 11) CreateMapTile(x, y, GO_LineLR);

                if (x == 4 && y == 0) CreateMapTile(x, y, GO_TurnUR);
                if (x == 4 && y == 1) CreateMapTile(x, y, GO_TurnDL);
                if (x == 4 && y == 3) CreateMapTile(x, y, GO_LineLR);
                if (x == 4 && y == 6) CreateMapTile(x, y, GO_LineLR);
                if (x == 4 && y == 9) CreateMapTile(x, y, GO_LineLR);
                if (x == 4 && y == 11) CreateMapTile(x, y, GO_LineLR);

                if (x == 5 && y == 0) CreateMapTile(x, y, GO_LineLR);
                if (x == 5 && y == 3) CreateMapTile(x, y, GO_LineLR);
                if (x == 5 && y == 6) CreateMapTile(x, y, GO_LineLR);
                if (x == 5 && y == 9) CreateMapTile(x, y, GO_LineLR);
                if (x == 5 && y == 11) CreateMapTile(x, y, GO_LineLR);

                if (x == 6 && y == 0) CreateMapTile(x, y, GO_TurnUL);
                if (x == 6 && y == 1) CreateMapTile(x, y, GO_CrossUDR);
                if (x == 6 && y == 2) CreateMapTile(x, y, GO_LineUD);
                if (x == 6 && y == 3) Switch5A = CreateMapTile(x, y, GO_LineLR, "Switch5A");
                if (x == 6 && y == 3) Switch5B = CreateMapTile(x, y, GO_TurnDL, "Switch5B");
                if (x == 6 && y == 6) CreateMapTile(x, y, GO_TurnUL);
                if (x == 6 && y == 7) CreateMapTile(x, y, GO_CrossUDR);
                if (x == 6 && y == 8) CreateMapTile(x, y, GO_LineUD);
                if (x == 6 && y == 9) Switch2A = CreateMapTile(x, y, GO_LineUD, "Switch2A");
                if (x == 6 && y == 9) Switch2B = CreateMapTile(x, y, GO_TurnUL, "Switch2B");
                if (x == 6 && y == 10) CreateMapTile(x, y, GO_LineUD);
                if (x == 6 && y == 11)
                {
                    Switch1A = CreateMapTile(x, y, GO_LineLR, "Switch1A");
                    trainController.AddTarget("Switch1", Switch1A.transform.position);
                }

                if (x == 6 && y == 11) Switch1B = CreateMapTile(x, y, GO_TurnDR, "Switch1B");

                if (x == 7 && y == 1) CreateMapTile(x, y, GO_LineLR);
                if (x == 7 && y == 3) CreateMapTile(x, y, GO_LineLR);
                if (x == 7 && y == 1) CreateMapTile(x, y, GO_LineLR);
                if (x == 7 && y == 7) CreateMapTile(x, y, GO_LineLR);
                if (x == 7 && y == 11) CreateMapTile(x, y, GO_LineLR);

                if (x == 8 && y == 1) CreateMapTile(x, y, GO_LineLR);
                if (x == 8 && y == 3) CreateMapTile(x, y, GO_LineLR);
                if (x == 8 && y == 7) CreateMapTile(x, y, GO_LineLR);
                if (x == 8 && y == 11) CreateMapTile(x, y, GO_LineLR);

                if (x == 9 && y == 1) CreateMapTile(x, y, GO_LineLR);
                if (x == 9 && y == 3) CreateMapTile(x, y, GO_TurnUL);
                if (x == 9 && y == 4) CreateMapTile(x, y, GO_TurnDR);
                if (x == 9 && y == 7) CreateMapTile(x, y, GO_LineLR);
                if (x == 9 && y == 11) CreateMapTile(x, y, GO_LineLR);

                if (x == 10 && y == 1) CreateMapTile(x, y, GO_LineLR);
                if (x == 10 && y == 4) CreateMapTile(x, y, GO_LineLR);
                if (x == 10 && y == 7) CreateMapTile(x, y, GO_LineLR);
                if (x == 10 && y == 11) CreateMapTile(x, y, GO_LineLR);

                if (x == 11 && y == 1) CreateMapTile(x, y, GO_LineLR);
                if (x == 11 && y == 4) CreateMapTile(x, y, GO_LineLR);
                if (x == 11 && y == 7) CreateMapTile(x, y, GO_LineLR);
                if (x == 11 && y == 11) CreateMapTile(x, y, GO_LineLR);


                if (x == 12 && y == 1) CreateMapTile(x, y, GO_LineLR);
                if (x == 12 && y == 4) CreateMapTile(x, y, GO_CrossLRU);
                if (x == 12 && y == 5) CreateMapTile(x, y, GO_LineUD);
                if (x == 12 && y == 6) CreateMapTile(x, y, GO_CrossUDR);
                if (x == 12 && y == 7) CreateMapTile(x, y, GO_TurnDL);
                if (x == 12 && y == 11) CreateMapTile(x, y, GO_LineLR);

                if (x == 13 && y == 1) Switch6A = CreateMapTile(x, y, GO_LineLR, "Switch6A");
                if (x == 13 && y == 1) Switch6B = CreateMapTile(x, y, GO_TurnUL, "Switch6B");
                if (x == 13 && y == 2) CreateMapTile(x, y, GO_LineUD);
                if (x == 13 && y == 3) CreateMapTile(x, y, GO_LineUD);
                if (x == 13 && y == 4) CreateMapTile(x, y, GO_TurnDL);
                if (x == 13 && y == 6) CreateMapTile(x, y, GO_LineLR);
                if (x == 13 && y == 11) CreateMapTile(x, y, GO_LineLR);

                if (x == 14 && y == 1) CreateMapTile(x, y, GO_LineLR);
                if (x == 14 && y == 6) CreateMapTile(x, y, GO_CrossLRU);
                if (x == 14 && y >= 7 && y < 11) CreateMapTile(x, y, GO_LineUD);
                if (x == 14 && y == 11)
                {
                    GO_Temp = CreateMapTile(x, y, GO_TurnDL);
                    trainController.AddTarget("RightUpTurn", GO_Temp.transform.position);
                }

                if (x == 15 && y == 1) CreateMapTile(x, y, GO_TurnUL);
                if (x == 15 && y >= 2 && y < 6) CreateMapTile(x, y, GO_LineUD);
                if (x == 15 && y == 6) CreateMapTile(x, y, GO_TurnDL);

            }
        }
        GO_TurnUL.SetActive(false);
        GO_TurnUR.SetActive(false);
        GO_TurnDR.SetActive(false);
        GO_TurnDL.SetActive(false);
        GO_LineUD.SetActive(false);
        GO_LineLR.SetActive(false);
        GO_CrossUDR.SetActive(false);
        GO_CrossLRU.SetActive(false);

        trainController.mapInitDone = true;
    }

    private void PlaceStations()
    {
        for (int y = 0; y < 12; y++)
        {
            for (int x = 0; x < 16; x++)
            {
                if (x == 3 && y == 2) CreateMapTile(x, y, GO_Station);
                if (x == 3 && y == 7) CreateMapTile(x, y, GO_Station);
                if (x == 7 && y == 2) CreateMapTile(x, y, GO_Station);
                if (x == 7 && y == 8) CreateMapTile(x, y, GO_Station);
                if (x == 13 && y == 5) CreateMapTile(x, y, GO_Station);
                if (x == 7 && y == 8) CreateMapTile(x, y, GO_Station);
                if (x == 15 && y == 10) CreateMapTile(x, y, GO_Station);

            }
        }

        GO_Station.SetActive(false);

        GO_Train.transform.position = new Vector3(14 * XOffset, 9 * YOffset, -0.01f);
    }


    private GameObject CreateMapTile(float X, float Y, GameObject Origin, string Name = "")
    {
        GameObject GO;
        GO = GameObject.Instantiate(Origin,transform);
        GO.transform.position = new Vector3(X * XOffset, Y * YOffset, Y * 0.05f);
        if (Name != "")
        {
            GO.name = Name;
            GO.SetActive(false);
        }
        else
        {
            GO.name += "_" + X.ToString() + "_" + Y.ToString();
        }
        return GO;
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Keypad1)) Switch1 = !Switch1;
        if (Input.GetKeyDown(KeyCode.Keypad2)) Switch2 = !Switch2;
        if (Input.GetKeyDown(KeyCode.Keypad3)) Switch3 = !Switch3;
        if (Input.GetKeyDown(KeyCode.Keypad4)) Switch4 = !Switch4;
        if (Input.GetKeyDown(KeyCode.Keypad5)) Switch5 = !Switch5;
        if (Input.GetKeyDown(KeyCode.Keypad6)) Switch6 = !Switch6;

        Switch1A.SetActive(Switch1);
        Switch1B.SetActive(!Switch1);
        Switch2A.SetActive(Switch2);
        Switch2B.SetActive(!Switch2);
        Switch3A.SetActive(Switch3);
        Switch3B.SetActive(!Switch3);
        Switch4A.SetActive(Switch4);
        Switch4B.SetActive(!Switch4);
        Switch5A.SetActive(Switch5);
        Switch5B.SetActive(!Switch5);
        Switch6A.SetActive(Switch6);
        Switch6B.SetActive(!Switch6);


    }
}
