using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Item
{
    [System.Serializable, CreateAssetMenu(fileName = "Trade Item", menuName = "JacDev/Item/Create Trade Item", order = 1)]
    public class TradeItem : ItemSettingBase    // 是否採用ScriptableObject?
    {
        public new int id
        {
            get => SettingManager.Singleton.ItemSetting.tradeItemList.IndexOf(this);
        }

        public float originBuyPrice = 100f;      // 初始購買價值
        public float originSellPrice = 200f;    // 初始售出價值

        // 價格浮動
        [Header("價格浮動設定")]
        [Range(0f, 1f)] public float defaultSellPriceMultiply = .1f;
        [Range(0f, 1f)] public float defaultBuyPriceMultiply = .1f;
    }

}
// 所有物品父類別
