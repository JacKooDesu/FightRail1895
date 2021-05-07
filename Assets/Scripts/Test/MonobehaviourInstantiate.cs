using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Entity;

namespace JacDev.Testing
{
    public class MonobehaviourInstantiate : MonoBehaviour
    {
        public Enemy enemy;
        void Start()
        {
            GameHandler.Singleton.entities.Add(enemy.BuildEntityObject());
        }

    }
}

