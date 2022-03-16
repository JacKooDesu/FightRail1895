using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Map;
using JacDev.Mod;
using JacDev.Entity;
using JacDev.Audio;
using JacDev.Item;

public class SettingManager : MonoBehaviour
{
    static SettingManager singleton = null;

    public static SettingManager Singleton
    {
        get
        {
            if (singleton != null)
                return singleton;
            else
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

    [SerializeField] ModList modList;
    public ModList ModList
    {
        get => modList;
    }

    [SerializeField] ItemList itemSetting;
    public ItemList ItemSetting
    {
        get => itemSetting;
    }

    [SerializeField] JacDev.Event.EventList eventList;
    public JacDev.Event.EventList EventList
    {
        get => eventList;
    }

    [SerializeField] SoundList bgmSetting;
    public SoundList BgmSetting
    {
        get => bgmSetting;
    }

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
}
