using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JacDev.Entity
{
    public class Shell : EntitySetting
    {
        public override Type EntityObjectType() => typeof(ShellObject);

        public override EntityObject BuildEntityObject()
        {
            GameObject g = new GameObject(entityName, EntityObjectType());
            ShellObject so = g.GetComponent<ShellObject>();
            so.Init(this);
            return so;
        }
    }
}

