using UnityEngine;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;


public class CloudSave : MonoBehaviour
{

    public int MainLevel;
    public int SubLevel;

    public GameObject GO1;
    public GameObject GO2;
    public GameObject GO3;
    public GameObject GO4;

 
    async void SaveData(int MainLevel,int SubLevel)
    {
        var data = new Dictionary<string, object>
        {
            {"MainLevel",MainLevel },
            {"SubLevel",SubLevel }
        };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
    }

    async void LoadData()
    {
        var data = new HashSet<string>
        {
          "MainLevel",
          "SubLevel"
        };
        var LoadedData = await CloudSaveService.Instance.Data.Player.LoadAsync(data);
        MainLevel = LoadedData["MainLevel"].Value.GetAs<int>();
        SubLevel = LoadedData["SubLevel"].Value.GetAs<int>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData(MainLevel+1, 2);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SaveData(0, 2);
        }

        if (MainLevel >= 1) GO1.SetActive(true); else GO1.SetActive(false);
        if (MainLevel >= 2) GO2.SetActive(true); else GO2.SetActive(false);
        if (MainLevel >= 3) GO3.SetActive(true); else GO3.SetActive(false);
        if (MainLevel >= 4) GO4.SetActive(true); else GO4.SetActive(false);
    }
}
