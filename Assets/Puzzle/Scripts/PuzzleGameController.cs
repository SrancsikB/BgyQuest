using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class PuzzleGameController : MonoBehaviour
{
    public int coinQuantity;
    [SerializeField] Sprite[] railwaySprites;
    [SerializeField] List<bool> savedData = new List<bool>();
    [SerializeField] GameObject secretNumber;
    PuzzleCard[] puzzleCards;
    
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

        FindFirstObjectByType<UICoin>().coinQuantity = coinQuantity;

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
            secretNumber.SetActive(true);
        }



    }
    
    

    void LoadData()
    {
        TextMeshProUGUI secretText = secretNumber.GetComponent<TextMeshProUGUI>();
        secretNumber.SetActive(false);
        switch (gameGroup)
        {
            

            case MapFlag.GameGroup.Railway:


                secretText.text = "7";

                try
                {
                    PlayerDataControl pDC = PlayerDataControl.Instance;
                    coinQuantity = pDC.coins;
                    savedData = PlayerDataControl.Instance.GetRailwayPuzzleData();
                }
                catch (System.Exception)
                {
                    Debug.Log("SB: Player data cannot be aquired, using public data");
                }
                
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



}
