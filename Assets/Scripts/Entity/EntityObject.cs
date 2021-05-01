using UnityEngine;

namespace JacDev.Entity
{
    public abstract class EntityObject : MonoBehaviour
    {
        [SerializeField]
        protected EntitySetting entitySetting;

        public virtual bool GameUpdate() => true;

        public virtual void Init(EntitySetting setting)
        {
            entitySetting = setting;
        }
    }
}
