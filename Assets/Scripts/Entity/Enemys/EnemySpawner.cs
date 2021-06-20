using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    public class EnemySpawner : EntitySpawner
    {
        [SerializeField]
        protected float radius = 30f;
        bool active = true;

        private void Update()
        {
            if (active)
            {
                foreach (RaycastHit hit in Physics.SphereCastAll(
                    spawnpoint.position, radius, Vector3.forward, 0f))
                {
                    if (hit.transform.GetComponent<TrainObject>())
                    {
                        StartCoroutine(Spawn(0));
                        active = false;
                    }
                }
            }
        }
    }
}

