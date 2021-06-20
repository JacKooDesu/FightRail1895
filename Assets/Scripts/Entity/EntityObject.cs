using UnityEngine;

namespace JacDev.Entity
{
    public abstract class EntityObject : MonoBehaviour
    {
        [SerializeField]
        protected EntitySetting entitySetting;

        public virtual bool GameUpdate() => true;
        public float health = 10f;

        public virtual void Init(EntitySetting setting)
        {
            entitySetting = setting;
        }

        public virtual void GetDamage(float damage)
        {
            health -= damage;
        }
    }
}
