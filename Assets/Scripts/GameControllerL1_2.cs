using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControllerL1_2 : MonoBehaviour
{
    [SerializeField] GameObject Card;
    List<int> ListOfNumbers = new List<int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FillList();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                GameObject NewCard = GameObject.Instantiate(Card);
                NewCard.transform.position = new Vector3(i, j);
                GameObject Number = NewCard.transform.GetChild(0).gameObject;

                int Rnd = Random.Range(0, ListOfNumbers.Count - 1);
                Number.GetComponent<TextMeshPro>().text = ListOfNumbers[Rnd].ToString();
                ListOfNumbers.RemoveAt(Rnd);
            }
        }
        Card.SetActive(false);
    }


    void FillList()
    {
        ListOfNumbers.Add(3);
        ListOfNumbers.Add(7);
        ListOfNumbers.Add(7);
        ListOfNumbers.Add(0);
        ListOfNumbers.Add(9);
        ListOfNumbers.Add(2);
        ListOfNumbers.Add(1);
        ListOfNumbers.Add(4);
        ListOfNumbers.Add(5);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
