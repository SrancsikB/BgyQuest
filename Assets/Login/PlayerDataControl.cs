using System.Collections.Generic;
using Unity.Services.CloudSave;
using UnityEngine;

[System.Serializable]
public class PlayerDataControl : MonoBehaviour
{

    //private static PlayerDataControl playerDataControl;
    public static PlayerDataControl Instance;



    public string playerName;
    public int coins;

    private List<bool> railwayPuzzleData = new List<bool>() { true, true, true, true, true, true, true, true, true };

    public List<bool> GetRailwayPuzzleData()
    {
        return railwayPuzzleData;
    }







    void Awake()
    {
        Instance = this;
    }




    public async void InitData() //Save init data for new users
    {
        var data = new Dictionary<string, object>
                {
                    {"CoinData",0},
                    {"PuzzleData_Railway",new List<bool>() { false, false, false, false, false, false, false, false, false }}
                };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        LoadData();

    }


    public async void LoadData()
    {
        List<bool> savedData = new List<bool>();
        var data = new HashSet<string>
            {
                "CoinData",
                "PuzzleData_Railway"
            };
        var LoadedData = await CloudSaveService.Instance.Data.Player.LoadAsync(data);
        coins = LoadedData["CoinData"].Value.GetAs<int>();
        railwayPuzzleData = LoadedData["PuzzleData_Railway"].Value.GetAs<List<bool>>();

    }



    public async void SaveCoinData(int savedData)
    {

        var data = new Dictionary<string, object>
                {
                    {"CoinData",savedData}
                };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);

    }



    #region "Puzzle"



    public async void SavePuzzleData(MapFlag.GameGroup gg, List<bool> savedData)
    {

        var data = new Dictionary<string, object>
                {
                    {"PuzzleData_" + gg.ToString(),savedData}
                };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);

    }
    #endregion
}
