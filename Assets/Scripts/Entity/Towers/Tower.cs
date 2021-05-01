using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JacDev.Entity
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Tower", menuName = "JacDev/Create Tower", order = 1)]
    public class Tower : EntitySetting
    {
        public override Type EntityObjectType() => typeof(TowerObject);

        public override EntityObject BuildEntityObject()
        {
            GameObject g = new GameObject(entityName,EntityObjectType());
            return g.GetComponent<TowerObject>();
        }
    }
}

