using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JacDev.Entity
{
    // 敵方單位所有設定
    [System.Serializable, CreateAssetMenu(fileName = "Enemy", menuName = "JacDev/Create Enemy", order = 1)]
    public class Enemy : EntitySetting
    {
        public override Type EntityObjectType() => typeof(EnemyObject);

        public override EntityObject BuildEntityObject()
        {
            GameObject g = Instantiate(prefab);
            g.name = entityName;
            EnemyObject eo = g.GetComponent<EnemyObject>();
            // eo.Init(this);
            return eo;
        }

        public int level = 1;

        public enum BloodType
        {
            Aboriginal,
            Japanese,
            Hakka
        }

        [SerializeField]
        BloodType bloodType;     // 種族

        public float movementSpeed = 5.0f;  // 移動速度
        public float health = 20.0f;    // 血量
        public float maxDet = 5f;   //最遠偵測距離
        public float damage = 5.0f;     // 基礎傷害，之後可寫成獨立類別，與種族寫在一起
        public float attackRange = 1f;  // 攻擊距離
        public float attackTime = 1.0f; // 攻擊花費時長(秒)

        [System.Serializable]
        class Drop
        {
            Item item;
            float dropRate;
        }

        [SerializeField]
        List<Drop> dropList = new List<Drop>();  // 掉落表



        void DamageCal()
        {

        }

    }

}

