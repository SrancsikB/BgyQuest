using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;

public class PuzzleProcess : MonoBehaviour
{
    PuzzleCard[] puzzleCards;

    async private void Awake()
    {
        puzzleCards = FindObjectsByType<PuzzleCard>(FindObjectsSortMode.InstanceID);

        await UnityServices.InitializeAsync();
        if (AuthenticationService.Instance.SessionTokenExists)
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                LoadData();
            }
            catch (System.Exception)
            {

            }

        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }



        bool allCardsInPlace = true;
        foreach (PuzzleCard item in puzzleCards)
        {
            if (item.transform.position != item.correctPosition || item.transform.eulerAngles.z != 0)
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


    async void SaveData()
    {
        foreach (PuzzleCard item in puzzleCards)
        {
            var data = new Dictionary<string, object>
                {
                    {"RailwayPuzzle_" + item.gameObject.name + "_Position",item.gameObject.transform.position},
                    {"RailwayPuzzle_" + item.gameObject.name + "_Rotation",item.gameObject.transform.rotation},
                };
            await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        }

    }

    async void LoadData()
    {
        foreach (PuzzleCard item in puzzleCards)
        {
            var data = new HashSet<string>
            {
                "RailwayPuzzle_" + item.gameObject.name + "_Position",
                "RailwayPuzzle_" + item.gameObject.name + "_Rotation"
            };
            var LoadedData = await CloudSaveService.Instance.Data.Player.LoadAsync(data);
            item.transform.position = LoadedData["RailwayPuzzle_" + item.gameObject.name + "_Position"].Value.GetAs<Vector3>();
            item.transform.rotation = LoadedData["RailwayPuzzle_" + item.gameObject.name + "_Rotation"].Value.GetAs<Quaternion>();
        }

    }
}
