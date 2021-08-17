using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Data
{
    [System.Serializable]
    public class PlayerData : MonoBehaviour
    {
        public string playerName = "None";          // 玩家名
        public JacDev.TimeUtil.Time playingTime = default;    // 遊玩時間

        public float money = 0f;        // 金錢

        public List<Item> inventory = new List<Item>();     // 道具
        public List<ModData> modDatas = new List<ModData>();    // Mod 綁定資料
        [Header("塔解鎖進度")]
        public List<int> towersGrade = new List<int>(); // 0代表未解鎖

        [Header("擊殺計數")]
        public int totalKill = 0;
        public List<int> typeKill = new List<int>();  // 依照種族表紀錄擊殺數量

        public void Init()
        {
            typeKill.Capacity = GameHandler.Singleton.enemyList.enemies.Count;
            towersGrade.Capacity = GameHandler.Singleton.towerList.towers.Count;
        }
    }

}
