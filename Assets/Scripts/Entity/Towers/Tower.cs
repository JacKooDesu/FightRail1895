using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JacDev.Entity
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Tower", menuName = "JacDev/Tower/Create Tower", order = 1)]
    public class Tower : EntitySetting
    {
        public override Type EntityObjectType() => typeof(TowerObject);

        public override EntityObject BuildEntityObject()
        {
            GameObject g = new GameObject(entityName, EntityObjectType());
            return g.GetComponent<TowerObject>();
        }
        public int price;   // 價格

        public Projectile projectile;
        public float attackRange = 1f;  // 攻擊距離
        public float attackTime = 1.0f; // 攻擊花費時長(秒)
    }
}

