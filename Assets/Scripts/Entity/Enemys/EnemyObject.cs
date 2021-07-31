using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JacDev.UI.GameScene;

namespace JacDev.Entity
{
    public class EnemyObject : EntityObject
    {
        [Header("攻擊設定")]
        public ProjectileSpawner launcher;
        // public Enemy enemy;
        // public static TrainLine target;
        public EntityObject target;
        [SerializeField] Animator ani;
        EntityObject attackTarget;
        Collider taretCol;

        bool hasAttack = false;
        float notAttack = 0;
        bool attacking = false; // 之後應以狀態寫成寫成Enum
        float attackTime = 0f;  // 攻擊時間(每次攻擊所花時間)
        public float changeTargetTime = 3f;

        [Header("UI Settings")]
        public Slider healthBar;


        private void Awake()
        {
            if (ani == null)
                ani = GetComponent<Animator>();
            maxHealth = ((Enemy)entitySetting).health;
            health = maxHealth;
        }

        private void LateUpdate()
        {
            TestMove();
            UpdateUI();

            if (health <= 0)
                OnDead();
        }

        public void TestMove()
        {
            Enemy setting = (Enemy)entitySetting;
            // if (target == null)
            //     return;

            if (taretCol == null || notAttack >= changeTargetTime)
            {
                ChangeAttackTarget();
                notAttack = 0;
            }

            if (attacking)
            {
                if (attackTime <= setting.attackTime)
                {
                    attackTime += Time.deltaTime;
                    return;
                }
                attacking = false;
                attackTime = 0f;
            }


            notAttack += Time.deltaTime;
            // need added attack range in future
            if ((taretCol.ClosestPoint(transform.position) - GetComponent<Collider>().ClosestPoint(target.transform.position)).magnitude >= setting.attackRange)
            {
                if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    transform.Translate(setting.movementSpeed * Vector3.forward * Time.deltaTime);
                    transform.LookAt(taretCol.ClosestPoint(transform.position));
                }

            }
            else
            {
                if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    ani.SetTrigger("Attack");
                    launcher.Launch(taretCol.ClosestPoint(transform.position));
                    attacking = true;
                    notAttack = 0;
                    taretCol = null;
                    hasAttack = true;
                }

            }
            // print(ani.GetCurrentAnimatorClipInfo(0)[0].clip);

            Ray r = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(r, out hit, 5f, 1 << 8))
            {
                // print(this.name);
                transform.Rotate(Vector3.up * 90);
            }
        }

        // 後續須改寫成可攻擊己方單位
        public void ChangeAttackTarget()
        {
            TrainLine trainLine = FindObjectOfType<TrainLine>();

            // Near Train
            float targetDistance = Vector3.Distance(trainLine.trains[0].transform.position, transform.position);
            int index = 0;
            for (int i = 0; i < trainLine.trains.Length; ++i)
            {
                if (targetDistance > Vector3.Distance(trainLine.trains[i].transform.position, transform.position))
                {
                    targetDistance = Vector3.Distance(trainLine.trains[i].transform.position, transform.position);
                    index = i;
                }
            }

            target = trainLine.trains[index];
            taretCol = trainLine.trains[index].GetComponent<Collider>();

            if (targetDistance > ((Enemy)entitySetting).maxDet && hasAttack)
            {
                GameHandler.Singleton.entities.Remove(this);
                Destroy(gameObject);
            }
            // taretCol = target.trains[Random.Range(0, target.trains.Length)].GetComponent<Collider>();
        }

        public void UpdateUI()
        {
            healthBar.transform.parent.LookAt(Camera.main.transform, Vector3.down);
            healthBar.value = Mathf.Clamp01(health / maxHealth);
        }

        public override bool GameUpdate()
        {
            return true;
        }

        public override void Init(EntitySetting setting)
        {
            base.Init(setting);
        }

        public void OnDead()
        {
            GameHandler.Singleton.money += (entitySetting as Enemy).dropMoney;
            Destroy(gameObject);
        }
    }
}
