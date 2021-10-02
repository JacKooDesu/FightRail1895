﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JacDev.Entity;
using JacDev.Data;

public class GameHandler : MonoBehaviour
{
    static GameHandler singleton = null;

    public static GameHandler Singleton
    {
        get
        {
            singleton = FindObjectOfType(typeof(GameHandler)) as GameHandler;

            if (singleton == null)
            {
                GameObject g = new GameObject("GameHandler");
                singleton = g.AddComponent<GameHandler>();
            }

            return singleton;
        }
    }

    public bool debugMode;

    [Header("遊戲中資料")]
    public int money;   // 後續是否整合進PlayerData?
    public int initMoney;

    static PlayerData playerData;
    public PlayerData PlayerData { get { return playerData; } }
    // WIP 音訊控制類別
    // public AudioHandler audioHandler;
    GameData gameData;

    public List<EntityObject> entities = new List<EntityObject>();

    [Header("設定")]
    public TowerList towerList;
    public EnemyList enemyList;

    [SerializeField] JacDev.Map.MapSetting mapSetting;
    public static JacDev.Map.MapSetting MapSetting
    {
        get => MapSetting;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Singleton != this)
            Destroy(gameObject);

        if (!hasAddSceneLoadAction)
            SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void Start()
    {
        money = initMoney;
    }

    static bool hasAddSceneLoadAction;
    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        hasAddSceneLoadAction = true;
        print($"載入 {scene.name}");
        switch (scene.name)
        {
            case "AsyncLoadingScene":
                //     Pause();
                //     break;

                // case "LoadGameScene":
                if (FileManager.Load("/PlayerData", "/PlayerData") != null)
                {
                    playerData = FileManager.Load("/PlayerData", "/PlayerData");
                }
                else
                {
                    playerData = new PlayerData();
                    FileManager.Save("/PlayerData", playerData, "/PlayerData");
                }
                break;

            case "ShopScene":
                if (FileManager.Load("/PlayerData", "/PlayerData") != null)
                {
                    playerData = FileManager.Load("/PlayerData", "/PlayerData");
                }
                else
                {
                    playerData = new PlayerData();
                    FileManager.Save("/PlayerData", playerData, "/PlayerData");
                }
                break;
        }
    }

    public void Pause(bool b)
    {
        Time.timeScale = b ? 0 : 1;
    }

    private void Update()
    {
        for (int i = 0; i < entities.Count; ++i)
        {
            if (!entities[i].GameUpdate())
            {
                entities.RemoveAt(i);
            }
        }
    }


}
