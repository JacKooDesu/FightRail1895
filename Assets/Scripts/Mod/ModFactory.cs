using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JacDev.Mod
{
    [System.Serializable, CreateAssetMenu(fileName = "new Mod", menuName = "JacDev/Mod/Create Mod", order = 1)]
    public class ModFactory : ScriptableObject
    {
        public string modName;

        public Entity.EntityEnums targetEntity;

        public List<ModSetting> copper = new List<ModSetting>();
        public List<ModSetting> silver = new List<ModSetting>();
        public List<ModSetting> gold = new List<ModSetting>();
        public List<ModSetting> purple = new List<ModSetting>();
        public List<ModSetting> rainbow = new List<ModSetting>();
    }

    [System.Serializable]
    public class ModSetting
    {
        public ModType modType = ModType.Tower_Damage;
        public float baseValue, max, min;

        public string ModToString()
        {
            // switch (modType)
            // {
            //     case ModType.Damage: return "傷害";

            //     case ModType.AtkSpeed: return "攻速";

            //     case ModType.Accurate: return "精準";

            //     case ModType.Poison: return "中毒";

            //     case ModType.Burn: return "燃燒";

            //     case ModType.Dizzy: return "暈眩";

            //     case ModType.Capacity: return "彈匣";
            // }

            return "MOD TYPE ERROR";
        }
    }
}

