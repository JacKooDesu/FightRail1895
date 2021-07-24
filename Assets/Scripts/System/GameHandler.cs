using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JacDev.Entity;

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
    
    PlayerData playerData;
    // WIP 音訊控制類別
    // public AudioHandler audioHandler;

    GameData gameData;

    public List<EntityObject> entities = new List<EntityObject>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Singleton != this)
            Destroy(gameObject);

        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "AsyncLoadingScene":
                //     Pause();
                //     break;

                // case "LoadGameScene":
                if (FileManager.Load("/PlayerData", "/PlayerData"))
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

    public void Pause()
    {
        Time.timeScale = 0;
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
