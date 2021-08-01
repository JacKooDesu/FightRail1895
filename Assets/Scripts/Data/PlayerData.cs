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
    }

}
