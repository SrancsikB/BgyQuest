using System.Collections.Generic;

using UnityEngine;

public class PuzzleProcess : MonoBehaviour
{
    [SerializeField] Sprite[] railwaySprites;
    PuzzleCard[] puzzleCards;
    List<bool> savedData = new List<bool>();
    public MapFlag.GameGroup gameGroup;

    
    private void Awake()
    {
        puzzleCards = FindObjectsByType<PuzzleCard>(FindObjectsSortMode.InstanceID);
    
    }


    void Start()
    {
        //Hide particles
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        LoadData();



    }

    private PuzzleCard FindCardByOrder(int order)
    {
        foreach (var item in puzzleCards)
        {

            if (item.order == order)
            {
                return item;
            }

        }

        return null;
    }



    void Update()
    {
       
       
        bool allCardsInPlace = true;
        foreach (PuzzleCard item in puzzleCards)
        {
            if (item.transform.position != item.correctPosition || (int)item.transform.eulerAngles.z != 0)
            {
                allCardsInPlace = false;
                return;
            }
        }

        if (allCardsInPlace)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }



    }
    
    

    void LoadData()
    {
        savedData = new List<bool>();

        switch (gameGroup)
        {
            
            case MapFlag.GameGroup.Railway:
                savedData = PlayerDataControl.Instance.GetRailwayPuzzleData();
                break;
            case MapFlag.GameGroup.Soldier:
                break;
            case MapFlag.GameGroup.Hero:
                break;
            default:
                break;
        }



       
        for (int i = 0; i < puzzleCards.Length; i++)
        {
            PuzzleCard puzzleCard = FindCardByOrder(i);
            puzzleCard.aquired = savedData[i];
            if (puzzleCard.aquired == true)
            {
                puzzleCard.GetComponent<SpriteRenderer>().sprite = railwaySprites[i];
            }

        }

        //Randomize puzzle position and rotation
        List<int> randomizedList = new List<int>();
        List<int> randomizedRot = new List<int>() { 0, 90, 180, 270 };


        for (int x = 1; x <= 3; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                bool found = false;
                while (!found)
                {
                    int rnd = Random.Range(0, puzzleCards.Length);
                    if (!randomizedList.Contains(rnd))
                    {
                        found = true;
                        randomizedList.Add(rnd);
                        puzzleCards[rnd].correctPosition = puzzleCards[rnd].transform.position;
                        puzzleCards[rnd].transform.position = new Vector3(x, y, 0);
                        if (puzzleCards[rnd].aquired)
                        {
                            int rndRot = Random.Range(0, randomizedRot.Count);
                            puzzleCards[rnd].transform.Rotate(0, 0, randomizedRot[rndRot]);
                        }
                        puzzleCards[rnd].StoreStartPos();

                    }
                }



            }
        }

        

    }





    //async void SaveData()
    //{

    //    savedData.Clear();
    //    for (int i = 0; i < puzzleCards.Length; i++)
    //    {
    //        PuzzleCard puzzleCard = FindCardByOrder(i);
    //        savedData.Add(puzzleCard.aquired);
    //    }

    //    var data = new Dictionary<string, object>
    //            {
    //                {"PuzzleData_Railway",savedData}
    //            };
    //    await CloudSaveService.Instance.Data.Player.SaveAsync(data);

    //}



    /*This can be good for Struct data save

    List<PuzzleData> puzzleData = new List<PuzzleData>();
    struct PuzzleData
    {
        public bool aquired;
        public Vector3 position;
        public Quaternion rotation;

    }




    async void SaveData()
    {
        
        puzzleData.Clear();

        foreach (PuzzleCard item in puzzleCards)
        {
            PuzzleData pd = new PuzzleData();
            pd.aquired=(item.aquired);
            pd.position=(item.transform.position);
            pd.rotation=(item.transform.rotation);
            puzzleData.Add(pd);
            
        }

        var data = new Dictionary<string, object>
                {
                    {"RailwayPuzzleData",puzzleData}
                };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);

    }

    async void LoadData()
    {
      
        var data = new HashSet<string>
            {
                "RailwayPuzzleData"
            };
        var LoadedData = await CloudSaveService.Instance.Data.Player.LoadAsync(data);
        puzzleData = LoadedData["RailwayPuzzleData"].Value.GetAs<List<PuzzleData>>();
        for (int i = 0; i < puzzleCards.Length; i++)
        {
            puzzleCards[i].aquired = puzzleData[i].aquired;
            puzzleCards[i].transform.position = puzzleData[i].position;
            puzzleCards[i].transform.rotation = puzzleData[i].rotation;
        }

    }

    */
}
