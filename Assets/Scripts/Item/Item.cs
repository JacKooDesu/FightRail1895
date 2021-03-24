using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 所有物品父類別
[System.Serializable, CreateAssetMenu(fileName = "ItemData", menuName = "JacDev/Create Item", order = 1)]
public class Item : ScriptableObject    // 是否採用ScriptableObject?
{
    public string itemName = "老二";    // 道具名稱
    [TextArea(3, 5)]
    public string description = "很長"; // 道具說明

    public int id = 0;      // 道具ID

    public Sprite icon;     // 依照之後是否為3D物件改為MeshRenderer

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
    public int currentStack = 0;    // 當前堆疊，之後可能改正到inventory裡面

    public bool droppable = false;   // 可掉落

    public bool saleable = true;    // 可販售
    public float value = 100f;      // 價值
}
