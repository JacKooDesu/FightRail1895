using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Item
{
    [CreateAssetMenu(fileName = "Item List", menuName = "JacDev/Item/Create Item List", order = 0)]
    public class ItemList : ScriptableObject
    {
        public List<TradeItem> tradeItemList;
        public List<DropItem> dropableItemList;
    }
}
