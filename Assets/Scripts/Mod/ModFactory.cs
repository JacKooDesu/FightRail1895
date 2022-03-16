using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JacDev.Mod
{
    [System.Serializable, CreateAssetMenu(fileName = "new Mod", menuName = "JacDev/Mod/Create Mod", order = 1)]
    public class ModFactory : ScriptableObject
    {
        public string modName;
        public ModType modType = ModType.Tower_Damage;
        public Sprite icon;

        [TextArea(3, 10)]
        public string description;

        public Entity.EntityEnums.EntityType targetEntity;

        public RankSetting copper = new RankSetting();
        public RankSetting silver = new RankSetting();
        public RankSetting gold = new RankSetting();
        public RankSetting purple = new RankSetting();
        public RankSetting rainbow = new RankSetting();

        public RankSetting GetRankSetting(int rk)
        {
            RankSetting[] rankSettings = {
                copper,
                silver,
                gold,
                purple,
                rainbow
            };

            return rankSettings[rk];
        }
    }

    [System.Serializable]
    public class RankSetting
    {
        public Sprite icon;
        public float baseValue;

        // public string ModToString()
        // {
        //     // switch (modType)
        //     // {
        //     //     case ModType.Damage: return "傷害";

        //     //     case ModType.AtkSpeed: return "攻速";

        //     //     case ModType.Accurate: return "精準";

        //     //     case ModType.Poison: return "中毒";

        //     //     case ModType.Burn: return "燃燒";

        //     //     case ModType.Dizzy: return "暈眩";

        //     //     case ModType.Capacity: return "彈匣";
        //     // }

        //     return "MOD TYPE ERROR";
        // }
    }
}

