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

        [Header("基礎數值設定")]
        public float copper;
        public float silver;
        public float gold;
        public float purple;
        public float rainbow;

        public float GetQualityValue(int rk)
        {
            float[] valueSettings = {
                    copper,
                    silver,
                    gold,
                    purple,
                    rainbow
                };

            return valueSettings[rk];
        }

        public int GetSettingIndex()
        {
            return SettingManager.Singleton.ModList.modList.IndexOf(this);
        }
    }

    [System.Serializable]
    public class RankSetting
    {
        public Sprite icon;
        public float baseValue;
    }
}

