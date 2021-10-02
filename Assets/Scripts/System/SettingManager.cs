﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Map;
using JacDev.Mod;
using JacDev.Entity;

public class SettingManager : MonoBehaviour
{
    static SettingManager singleton = null;

    public static SettingManager Singleton
    {
        get
        {
            singleton = FindObjectOfType(typeof(SettingManager)) as SettingManager;

            if (singleton == null)
            {
                GameObject g = new GameObject("SettingManager");
                singleton = g.AddComponent<SettingManager>();
            }
            return singleton;
        }
    }

    [SerializeField] MapSetting mapSetting;
    public MapSetting MapSetting
    {
        get => mapSetting;
    }

    [SerializeField] ModFactory modSetting;
    public ModFactory ModSetting
    {
        get => modSetting;
    }

    [SerializeField] TowerList towerSetting;
    public TowerList TowerSetting
    {
        get => towerSetting;
    }

    [SerializeField] EnemyList enemySetting;
    public EnemyList EnemySetting
    {
        get => enemySetting;
    }
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
