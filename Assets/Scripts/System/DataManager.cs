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
    [SerializeField] MapData mapData;
    [SerializeField] ModData modData;

    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public MapData GetMapData(bool regenerate = false)
    {
        if (regenerate)
        {
            mapData = SettingManager.Singleton.MapSetting.InitMap();
            FileManager.Save("/TestMap", mapData, "/MapData");
        }
        else
        {
            if (FileManager.Load<MapData>("/MapData", "/TestMap") as MapData != null)
            {
                mapData = FileManager.Load<MapData>("/MapData", "/TestMap");
            }
            else
            {
                mapData = SettingManager.Singleton.MapSetting.InitMap();
                FileManager.Save("/TestMap", mapData, "/MapData");
            }
        }

        return mapData;
    }
}
