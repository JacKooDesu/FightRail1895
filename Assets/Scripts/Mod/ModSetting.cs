using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Mod
{
    [System.Serializable]
    public class ModSetting
    {
        public ModType modType = ModType.Damage;
        [Range(-1f, 1f)]
        public float amount;

        public string ModToString()
        {
            switch (modType)
            {
                case ModType.Damage: return "傷害";

                case ModType.AtkSpeed: return "攻速";

                case ModType.Accurate: return "精準";

                case ModType.Poison: return "中毒";

                case ModType.Burn: return "燃燒";

                case ModType.Dizzy: return "暈眩";

                case ModType.Capacity: return "彈匣";
            }

            return "MOD TYPE ERROR";
        }
    }
}

