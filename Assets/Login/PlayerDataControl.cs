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
        Instance = this;
    }


    public string playerName;
    public int coins;

    //Railway
    [System.Serializable]
    public struct RailwayPogressData
    {
        public int rwMapLevel; //Level of map/scene
        public int rwTrainSpeed; //Speed of the train
        public float rwSwitchingTime; //Speed of rw swithcing time
        public int rwWagonLevel; //Number of wagons
        public int rwCoalHeapLevel; //Size and freq of coal heaps
        public int rwBonusCoinLevel; //Level of bonus coin

        public RailwayPogressData(int rwMapLevel, int rwTrainSpeed, float rwSwitchingTime, int rwWagonLevel, int rwCoalHeapLevel, int rwBonusCoinLevel)
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
                    {"CoinData",0},
                    {"PuzzleData_rw",new List<bool>() { false, false, false, false, false, false, false, false, false }},
                    {"ProgressData_rw",new RailwayPogressData(1,1,5,1,0,0)}
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
                "PuzzleData_rw",
                "ProgressData_rw"
            };
        var LoadedData = await CloudSaveService.Instance.Data.Player.LoadAsync(data);
        coins = LoadedData["CoinData"].Value.GetAs<int>();
        rwPuzzleData = LoadedData["PuzzleData_rw"].Value.GetAs<List<bool>>();
        rwProgressData = LoadedData["ProgressData_rw"].Value.GetAs<RailwayPogressData>();

    }



    public async void SaveCoinData(int savedData)
    {

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
                    {"ProgressData_rw",railwayPD}
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
