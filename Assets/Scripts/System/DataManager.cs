using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Data;

public class DataManager : MonoBehaviour
{
    // Singleton
    static DataManager singleton = null;

    public static DataManager Singleton
    {
        get
        {
            if (singleton != null)
                return singleton;
            else
                singleton = FindObjectOfType(typeof(DataManager)) as DataManager;

            if (singleton == null)
            {
                GameObject g = new GameObject("DataManager");
                singleton = g.AddComponent<DataManager>();
            }
            return singleton;
        }
    }

    #region DATA_VARIABLES
    [SerializeField] PlayerData playerData;
    public PlayerData PlayerData
    {
        get
        {
            return playerData;
        }
        set
        {
            playerData = value;
            FileManager.Save("/PlayerData", playerData, "/GameDatas");
        }
    }
    [SerializeField] MapData mapData;
    [SerializeField] ModData modData;

    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }

        LoadPlayerData();
    }

    public MapData GetMapData(bool regenerate = false)
    {
        if (regenerate)
        {
            mapData = SettingManager.Singleton.MapSetting.InitMap();
            FileManager.Save("/TestMap", mapData, "/GameDatas");
        }
        else
        {
            if (FileManager.Load<MapData>("/GameDatas", "/TestMap") as MapData != null)
            {
                mapData = FileManager.Load<MapData>("/GameDatas", "/TestMap");
            }
            else
            {
                mapData = SettingManager.Singleton.MapSetting.InitMap();
                FileManager.Save("/TestMap", mapData, "/GameDatas");
            }
        }

        return mapData;
    }

    public void LoadPlayerData()
    {
        if (FileManager.Load<PlayerData>("/GameDatas", "/PlayerData") != null)
        {
            PlayerData = FileManager.Load<PlayerData>("/GameDatas", "/PlayerData");
        }
        else
        {
            PlayerData = null;
        }

    }

    public void SavePlayerData()
    {
        if (PlayerData != null)
        {
            FileManager.Save("/PlayerData", PlayerData, "/GameDatas");
        }
    }
}
