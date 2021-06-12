﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JacDev.Entity
{
    [System.Serializable, CreateAssetMenu(fileName = "Projectile", menuName = "JacDev/Create Projectile", order = 1)]
    public class Projectile : EntitySetting
    {
        public override Type EntityObjectType() => typeof(ProjectileObject);

        public override EntityObject BuildEntityObject()
        {
            GameObject g = Instantiate(prefab);
            g.name = entityName;
            ProjectileObject so = g.GetComponent<ProjectileObject>();
            // so.Init(this);
            return so;
        }
    }
}
