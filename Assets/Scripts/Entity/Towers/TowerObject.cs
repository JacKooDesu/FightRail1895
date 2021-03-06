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
        Animator animator;

        private void OnEnable()
        {
            if (GetComponentInChildren<Animator>() != null)
                animator = GetComponentInChildren<Animator>();

            launcher.owner = this;

            CalStatus();
        }

        // 讀取玩家升級狀態重新計算數值
        void CalStatus()
        {
            var setting = (entitySetting as Tower);
            var index = SettingManager.Singleton.TowerSetting.towers.IndexOf(setting);
            var grade = DataManager.Singleton.PlayerData.towersGrade[index];
            // 傷害公式還須被定義!!
            damage = setting.damage * (1 + setting.upgradeMultiply.damage * (float)grade);
        }

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
                    int target = Random.Range(0, enemyObjects.Count);
                    Vector3 targetPoint = enemyObjects[target].GetComponent<Collider>().ClosestPoint(launcher.transform.position);
                    launcher.Launch(targetPoint);
                    // transform.localRotation = Quaternion.LookRotation(targetPoint);
                    transform.LookAt(new Vector3(targetPoint.x, transform.position.y, targetPoint.z));

                    if (animator != null)
                        animator.SetTrigger("Attack");

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

    }
}