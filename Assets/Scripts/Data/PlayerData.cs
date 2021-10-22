using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Data
{
    [System.Serializable]
    public class PlayerData
    {
        public string playerName = "None";          // 玩家名
        public JacDev.TimeUtil.Time playingTime = default;    // 遊玩時間

        public Entity.EntityEnums.BloodType bloodType;

        public float money = 0f;        // 金錢

        public List<Item> inventory = new List<Item>();     // 道具
        public List<ModData> modDatas = new List<ModData>();    // Mod 綁定資料
        [Header("塔解鎖進度")]
        public List<int> towersGrade = new List<int>(); // 0代表未解鎖

        [Header("擊殺計數")]
        public int totalKill = 0;
        public List<int> typeKill = new List<int>();  // 依照種族表紀錄擊殺數量

        [Header("遊戲進度")]
        public Map.Station currentStation;

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

            currentStation = startStation;
        }
    }

}
