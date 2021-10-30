using UnityEngine;
using System;

namespace JacDev.Entity
{
    public abstract class EntitySetting : ScriptableObject
    {
        [SerializeField, Header("名稱")]
        public string entityName;
        public string entityNickName;
        [TextArea(4, 8)] public string entityInfo;

        [SerializeField, Header("物件種類")]
        public EntityEnums.EntityType entityType = default;

        [SerializeField, Header("圖示")]
        public Sprite icon;

        [SerializeField, Header("預製物")]
        public GameObject prefab;

        public abstract Type EntityObjectType();

        public abstract EntityObject BuildEntityObject();
        // public EntityObject GetEntityObject()
        // {
        //     switch (entityType)
        //     {
        //         case EntityType.Enemy:
        //             return new EnemyObject();

        //         case EntityType.Tower:
        //             return new TowerObject();

        //         // case EntityType.Train:
        //         //     return new TrainObject();

        //         default:
        //             return null;
        //     }
        // }
    }
}
