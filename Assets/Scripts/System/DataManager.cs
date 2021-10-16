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
}
