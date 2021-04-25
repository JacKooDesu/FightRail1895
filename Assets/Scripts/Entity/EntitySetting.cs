using UnityEngine;

namespace JacDev.Entity
{
    public abstract class EntitySetting : ScriptableObject
    {
        public string entityName;

        [Header("物件種類")]
        EntityType type = default;

        [Header("圖示")]
        Sprite icon;
    }
}
