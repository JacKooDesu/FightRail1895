using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public string playerName = "None";          // 玩家名
    public JacDev.TimeUtil.Time playingTime = default;    // 遊玩時間

    public float money = 0f;        // 金錢

    public List<Item> inventory = new List<Item>();     // 道具
}
