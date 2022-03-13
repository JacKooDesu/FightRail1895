using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JacDev.Item;

namespace JacDev.Entity
{
    // 敵方單位所有設定
    [System.Serializable, CreateAssetMenu(fileName = "Enemy", menuName = "JacDev/Enemy/Create Enemy", order = 1)]
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

        [Header("數值設定")]
        // public int level = 1;
        public AnimationCurve levelCurve;   // 等級曲線，依照遊戲進度提升的曲線

        [SerializeField]
        EntityEnums.BloodType bloodType;     // 種族

        public float movementSpeed = 5.0f;  // 移動速度
        public float health = 20.0f;    // 血量
        public float maxDet = 5f;   //最遠偵測距離
        public AttackType attackType = AttackType.Melee; // 攻擊模式
        public Projectile projectile;   // 彈藥
        public float damage = 5.0f;     // 基礎傷害，之後可寫成獨立類別，與種族寫在一起
        public float attackRange = 1f;  // 攻擊距離
        public float attackTime = 1.0f; // 攻擊花費時長(秒)
        public float attackTimeOffset = .75f;   // 攻擊判定時間點


        [Header("掉落")] public DropTable dropTable;  // 掉落表

        public int dropMoney = 20;  // 擊殺掉落金錢

        void DamageCal()
        {

        }

    }

}

