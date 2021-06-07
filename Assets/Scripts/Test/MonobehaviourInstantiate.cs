using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Entity;

namespace JacDev.Testing
{
    public class MonobehaviourInstantiate : MonoBehaviour
    {
        public Enemy enemy;
        public Projectile projectile;
        void Start()
        {
            GameHandler.Singleton.entities.Add(enemy.BuildEntityObject());
            GameHandler.Singleton.entities.Add(projectile.BuildEntityObject());
        }

    }
}

