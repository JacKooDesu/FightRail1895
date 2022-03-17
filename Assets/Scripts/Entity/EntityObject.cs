using UnityEngine;
using System;

namespace JacDev.Entity
{
    public abstract class EntityObject : MonoBehaviour
    {
        public EntitySetting entitySetting;

        public virtual bool GameUpdate() => true;
        public float maxHealth = 10f;
        public float health = 10f;

        // 計算後的數值
        [SerializeField] protected float damage;
        public float Damage
        {
            get => damage;
        }
        [SerializeField] protected float attackTime;
        public float AttackTime
        {
            get => damage;
        }
        [SerializeField] protected float range;
        public float Range
        {
            get => range;
        }

        [HideInInspector] public event Action onGetDamage;

        public int id;  // 用於GameHandler管理物件

        // public virtual void Init(EntitySetting setting)
        public virtual void Init()
        {
            // entitySetting = setting;
        }

        public virtual void GetDamage(float damage)
        {
            health -= damage;
            if (onGetDamage != null)
                onGetDamage.Invoke();
        }

        protected void OnDestroy()
        {
            GameHandler.Singleton.entities.Remove(this);
        }
    }
}
