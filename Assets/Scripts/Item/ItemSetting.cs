using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Item
{
    [System.Serializable, CreateAssetMenu(fileName = "ItemData", menuName = "JacDev/Item/Create Item", order = 1)]
    public class ItemSetting : ScriptableObject    // 是否採用ScriptableObject?
    {
        [Header("物品名稱")]
        public string itemName;    // 道具名稱

        [Header("說明"), TextArea(3, 5)]
        public string description; // 道具說明

        public int id = 0;      // 道具ID，應在後續版本刪除

        [Header("圖")]
        public Sprite icon;     // 依照之後是否為3D物件改為MeshRenderer
        public GameObject prefab;   // 模型prefab

        public enum Rarity  //物品稀有度，目前比照POE
        {
            Normal = 0,
            Unique,
            Rare,
            Magic
        }

        public Rarity rarity = Rarity.Normal;
        static public Color[] rarityColors = {
        new Color(.66f,.66f,.66f),
        new Color(.9f,.54f,.21f),
        new Color(.94f,.9f,.45f),
        new Color(.94f,.45f,.87f)
    };

        public bool stackable = false;  // 可堆疊
        public int maxStack = 16;       // 最高堆疊
        // public int currentStack = 0;    // 當前堆疊，之後可能改正到inventory裡面

        public bool droppable = false;   // 可掉落

        public bool saleable = true;    // 可販售
        public float price = 100f;      // 價值
    }

}
// 所有物品父類別
