using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JacDev.Entity
{
    public class EnemyObject : EntityObject
    {
        // public Enemy enemy;
        public static TrainLine target;
        Animator ani;
        EntityObject attackTarget;
        Collider taretCol;

        bool hasAttack = false;
        float notAttack = 0;
        public float changeTargetTime = 3f;

        [Header("UI Settings")]
        public Slider healthBar;


        private void Awake()
        {
            ani = GetComponent<Animator>();
            maxHealth = ((Enemy)entitySetting).health;
            health = maxHealth;
        }

        private void LateUpdate()
        {
            TestMove();
            UpdateUI();

            if (health <= 0)
                Destroy(gameObject);
        }

        public void TestMove()
        {
            if (target == null)
                return;

            if (taretCol == null || notAttack >= changeTargetTime)
            {
                ChangeAttackTarget();
                notAttack = 0;
            }


            notAttack += Time.deltaTime;
            // need added attack range in future
            if ((taretCol.ClosestPoint(transform.position) - GetComponent<Collider>().ClosestPoint(target.transform.position)).magnitude >= 1f)
            {
                if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    transform.Translate(((Enemy)entitySetting).movementSpeed * Vector3.forward * Time.deltaTime);
                    transform.LookAt(taretCol.ClosestPoint(transform.position));
                }

            }
            else
            {
                if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    ani.SetTrigger("Attack");
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

        public void ChangeAttackTarget()
        {
            if (target == null)
                return;

            // Near Train
            float targetDistance = Vector3.Distance(target.trains[0].transform.position, transform.position);
            int index = 0;
            for (int i = 0; i < target.trains.Length; ++i)
            {
                if (targetDistance > Vector3.Distance(target.trains[i].transform.position, transform.position))
                {
                    targetDistance = Vector3.Distance(target.trains[i].transform.position, transform.position);
                    index = i;
                }
            }

            taretCol = target.trains[index].GetComponent<Collider>();

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
    }

}
