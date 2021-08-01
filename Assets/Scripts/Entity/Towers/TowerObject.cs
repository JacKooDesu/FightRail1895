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
        public ProjectileSpawner launcher;

        private void Update()
        {
            Tower setting = (Tower)entitySetting;
            List<EnemyObject> enemyObjects = new List<EnemyObject>();

            if (canAttack)
            {
                // 2021.7.9 is now only target one enemy
                enemyObjects.Clear();
                foreach (RaycastHit hit in Physics.SphereCastAll(
                        transform.position, setting.attackRange, Vector3.forward, 0f))
                {
                    if (hit.transform.GetComponent<EnemyObject>())
                    {
                        // 2021.7.9   has move to projectile
                        // hit.transform.GetComponent<EnemyObject>().GetDamage(setting.damage);
                        // enemyObjects.Add(hit.transform.GetComponent<EnemyObject>());

                        // 2021.7.9   新增，之後須將精確度加入
                        enemyObjects.Add(hit.transform.GetComponent<EnemyObject>());
                    }
                }

                if (enemyObjects.Count != 0)
                {
                    launcher.Launch(enemyObjects[Random.Range(0, enemyObjects.Count)].transform.position);

                    canAttack = false;
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
    
        private void OnMouseDown() {
            
        }
    }
}