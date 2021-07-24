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

        [HideInInspector] public event Action onGetDamage;

        public virtual void Init(EntitySetting setting)
        {
            entitySetting = setting;
        }

        public virtual void GetDamage(float damage)
        {
            health -= damage;
            if (onGetDamage != null)
                onGetDamage.Invoke();
        }
    }
}
