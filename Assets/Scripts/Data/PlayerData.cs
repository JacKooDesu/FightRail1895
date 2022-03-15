using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Item;

namespace JacDev.Data
{
    [System.Serializable]
    public class PlayerData
    {
        public string playerName = "None";          // 玩家名
        public JacDev.TimeUtil.Time playingTime = default;    // 遊玩時間

        public Entity.EntityEnums.BloodType bloodType;

        public float money = 0f;        // 金錢

        public List<int> inventory = new List<int>();     // 道具，後續用int代替(由item list 取值)
        public int modCapacity = 28;     // Mod容量，小龍說先來個28張

        public List<ModData> trainModDatas = new List<ModData>();    // Mod 綁定資料
        public List<ModData> cabinModDatas = new List<ModData>();    // Mod 綁定資料
        public List<ModData> towerModDatas = new List<ModData>();    // Mod 綁定資料

        public void SortMod()
        {
            List<ModData> allMod = new List<ModData>();
            allMod.AddRange(trainModDatas);
            allMod.AddRange(cabinModDatas);
            allMod.AddRange(towerModDatas);

            trainModDatas = new List<ModData>();
            cabinModDatas = new List<ModData>();
            towerModDatas = new List<ModData>();
            foreach (var mod in allMod)
            {
                switch (mod.ToModFactory().targetEntity)
                {
                    case Entity.EntityEnums.EntityType.Train:
                        trainModDatas.Add(mod);
                        break;

                    case Entity.EntityEnums.EntityType.Cabin:
                        cabinModDatas.Add(mod);
                        break;

                    case Entity.EntityEnums.EntityType.Tower:
                        towerModDatas.Add(mod);
                        break;
                }
            }
        }


        [Header("塔解鎖進度")]
        public List<int> towersGrade = new List<int>(); // 0代表未解鎖

        [Header("擊殺計數")]
        public int totalKill = 0;
        public List<int> typeKill = new List<int>();  // 依照種族表紀錄擊殺數量

        [Header("遊戲進度")]
        public string currentStation;
        public string nextStation;
        public string currentPath;
        public int hasMoveCount;    // 計數玩家經過站點數量，用於計算敵方單位等級

        public enum State
        {
            InStation,
            OnPath
        }

        public State state;

        public PlayerData()
        {
            //this.playerName = name;
        }

        public void Init(string name, int bloodIndex, float money, Map.Station startStation)
        {
            typeKill.Capacity = SettingManager.Singleton.EnemySetting.enemies.Count;
            towersGrade.Capacity = SettingManager.Singleton.TowerSetting.towers.Count;

            this.playerName = name;
            this.bloodType = (Entity.EntityEnums.BloodType)bloodIndex;
            this.money = money;

            currentStation = startStation.GUID;

            // towersGrade = new List<int>(SettingManager.Singleton.TowerSetting.towers.Count);
            towersGrade.Add(1);
            for (int i = 1; i < SettingManager.Singleton.TowerSetting.towers.Count; ++i)
                towersGrade.Add(0);
        }

        public void AddMod(ModData mod)
        {
            switch (mod.ToModFactory().targetEntity)
            {
                case Entity.EntityEnums.EntityType.Tower:
                    towerModDatas.Add(mod);
                    return;

                case Entity.EntityEnums.EntityType.Cabin:
                    cabinModDatas.Add(mod);
                    return;

                case Entity.EntityEnums.EntityType.Train:
                    trainModDatas.Add(mod);
                    return;
            }
        }
    }

}
