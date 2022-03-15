using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Mod
{
    [System.Serializable, CreateAssetMenu(fileName = "Mod Quality Setting", menuName = "JacDev/Mod/new Quality Setting", order = 1)]
    public class ModQualityFactory : ScriptableObject
    {
        [System.Serializable]
        public class QualitySetting
        {
            public Sprite copper;
            public Sprite silver;
            public Sprite gold;
            public Sprite purple;
            public Sprite rainbow;

            public Sprite GetQualityIcon(int rk)
            {
                Sprite[] rankSettings = {
                    copper,
                    silver,
                    gold,
                    purple,
                    rainbow
                };

                return rankSettings[rk];
            }
        }

        public QualitySetting cabin = new QualitySetting();
        public QualitySetting tower = new QualitySetting();
        public QualitySetting train = new QualitySetting();

        public QualitySetting GetQualitySetting(ModFactory mod)
        {
            switch (mod.targetEntity)
            {
                case Entity.EntityEnums.EntityType.Tower:
                    return tower;

                case Entity.EntityEnums.EntityType.Cabin:
                    return cabin;

                case Entity.EntityEnums.EntityType.Train:
                    return train;
            }

            UnityEngine.Debug.LogWarning("不可使用Mod的Entity");
            return null;
        }
    }
}