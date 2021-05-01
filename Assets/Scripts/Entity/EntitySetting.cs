using UnityEngine;
using System;

namespace JacDev.Entity
{
    public abstract class EntitySetting : ScriptableObject
    {
        [SerializeField, Header("名稱")]
        protected string entityName;

        [SerializeField, Header("物件種類")]
        protected EntityType entityType = default;

        [SerializeField, Header("圖示")]
        protected Sprite icon;

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
