using UnityEngine;
using System.Collections.Generic;

namespace JacDev.Entity
{
    public class TowerObject : EntityObject
    {
        EntityObject attackTarget;
        TowerTargetingMode targetingMode;
        float cooldownTime = 0f;
        bool canAttack = true;

        private void Update()
        {
            Tower setting = (Tower)entitySetting;
            List<EnemyObject> enemyObjects = new List<EnemyObject>();

            if (canAttack)
            {
                foreach (RaycastHit hit in Physics.SphereCastAll(
                        transform.position, setting.attackRange, Vector3.forward, 0f))
                {
                    if (hit.transform.GetComponent<EnemyObject>())
                    {
                        hit.transform.GetComponent<EnemyObject>().GetDamage(setting.damage);
                        // enemyObjects.Add(hit.transform.GetComponent<EnemyObject>());
                        canAttack = false;
                    }
                }
            }
            else
            {
                if (cooldownTime < setting.attackTime)
                    cooldownTime += Time.deltaTime;
                else
                {
                    canAttack = true;
                    cooldownTime = 0f;
                }

            }


        }

        public override bool GameUpdate()
        {
            return true;
        }
    }
}