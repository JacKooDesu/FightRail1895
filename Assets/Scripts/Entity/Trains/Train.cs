using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    [System.Serializable, CreateAssetMenu(fileName = "Train", menuName = "JacDev/Create Train", order = 1)]
    public class Train : EntitySetting
    {
        public override Type EntityObjectType() => typeof(TrainObject);
        public override EntityObject BuildEntityObject()
        {
            GameObject g = Instantiate(prefab);
            g.name = entityName;
            TrainObject eo = g.GetComponent<TrainObject>();
            // eo.Init(this);
            return eo;
        }

        public float movementSpeed = 5.0f;  // 移動速度
        public float health = 500.0f;    // 血量
    }

}
