using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Item
{
    [System.Serializable, CreateAssetMenu(fileName = "Drop Item", menuName = "JacDev/Item/Create Drop Item", order = 1)]
    public class DropItem : ItemSettingBase
    {
        public Rarity rarity = Rarity.Normal;
    }
}