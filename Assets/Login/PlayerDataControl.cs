using System.Collections.Generic;
using Unity.Services.CloudSave;
using UnityEngine;

[System.Serializable]
public class PlayerDataControl : MonoBehaviour
{



    //private static PlayerDataControl playerDataControl;
    public static PlayerDataControl Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }


    public string playerName;
    public int coins;
    public bool lockQuiz; //Lock quiz button after NOK answer, release it after game start

    //Railway
    [System.Serializable]
    public struct RailwayPogressData
    {
        public int rwMapLevel; //Level of map/scene
        public int rwTrainSpeed; //Speed of the train
        public int rwSwitchingTime; //Speed of rw swithcing time
        public int rwWagonLevel; //Number of wagons
        public int rwCoalHeapLevel; //Size and freq of coal heaps
        public int rwBonusCoinLevel; //Level of bonus coin

        public RailwayPogressData(int rwMapLevel, int rwTrainSpeed, int rwSwitchingTime, int rwWagonLevel, int rwCoalHeapLevel, int rwBonusCoinLevel)
        {
            this.rwMapLevel = rwMapLevel;
            this.rwTrainSpeed = rwTrainSpeed;
            this.rwSwitchingTime = rwSwitchingTime;
            this.rwWagonLevel = rwWagonLevel;
            this.rwCoalHeapLevel = rwCoalHeapLevel;
            this.rwBonusCoinLevel = rwBonusCoinLevel;
        }
    }
    private List<bool> rwPuzzleData = new List<bool>() { true, true, true, true, true, true, true, true, true }; //Collected cards
    private RailwayPogressData rwProgressData;

    public List<bool> GetRailwayPuzzleData()
    {
        return rwPuzzleData;
    }
    public void SetRailwayPuzzleData(int index, bool aquired)
    {
        rwPuzzleData[index] = aquired;
        SavePuzzleData(MapFlag.GameGroup.Railway, rwPuzzleData);
    }

    public RailwayPogressData GetRailwayProgressData()
    {
        return rwProgressData;

    }

    public void SetRailwayProgressData(RailwayPogressData railwayPD)
    {
        rwProgressData = railwayPD;
        SaveRailwayProgressData(rwProgressData);
    }








    public async void InitData() //Save init data for new users
    {
        var data = new Dictionary<string, object>
                {
                    {"PlayerName",playerName},
                    {"CoinData",0},
                    {"PuzzleData_Railway",new List<bool>() { false, false, false, false, false, false, false, false, false }},
                    {"ProgressData_Railway",new RailwayPogressData(1,1,1,1,1,1)}
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
                "PuzzleData_Railway",
                "ProgressData_Railway"
            };
        var LoadedData = await CloudSaveService.Instance.Data.Player.LoadAsync(data);
        coins = LoadedData["CoinData"].Value.GetAs<int>();
        rwPuzzleData = LoadedData["PuzzleData_Railway"].Value.GetAs<List<bool>>();
        rwProgressData = LoadedData["ProgressData_Railway"].Value.GetAs<RailwayPogressData>();

    }



    public async void SaveCoinData(int savedData)
    {
        coins = savedData;
        var data = new Dictionary<string, object>
                {
                    {"CoinData",savedData}
                };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);

    }



    public async void SaveRailwayProgressData(RailwayPogressData railwayPD)
    {

        var data = new Dictionary<string, object>
                {
                    {"ProgressData_Railway",railwayPD}
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
