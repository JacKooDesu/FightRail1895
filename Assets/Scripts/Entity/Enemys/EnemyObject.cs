using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    public class EnemyObject : MonoBehaviour
    {
        public Enemy enemy;
        public TrainLine target;
        Animator ani;
        Collider taretCol;

        private void Awake()
        {
            ani = GetComponent<Animator>();
        }

        private void LateUpdate()
        {
            TestMove();
        }

        public void TestMove()
        {
            // transform.Translate(enemy.movementSpeed * Vector3.forward * Time.deltaTime);
            if (target != null)
            {
                if (taretCol == null)
                    ChangeAttackTarget();

                if ((taretCol.ClosestPoint(transform.position) - GetComponent<Collider>().ClosestPoint(target.transform.position)).magnitude >= .5f)
                {
                    if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        transform.Translate(enemy.movementSpeed * Vector3.forward * Time.deltaTime);
                        transform.LookAt(taretCol.ClosestPoint(transform.position));
                    }

                }
                else
                {
                    if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        ani.SetTrigger("Attack");
                        taretCol = null;
                    }

                }
                print(ani.GetCurrentAnimatorClipInfo(0)[0].clip);

                Ray r = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(r, out hit, 5f, 1 << 8))
                {
                    // print(this.name);
                    transform.Rotate(Vector3.up * 90);
                }
            }
        }

        public void SetEnemy(Enemy e)
        {
            enemy = e;
        }

        public void ChangeAttackTarget()
        {
            // Near Train
            // float targetDistance = Vector3.Distance(target.trains[0].transform.position, transform.position);
            // taretCol = target.trains[0].GetComponent<Collider>();
            // for (int i = 1; i < target.trains.Length; ++i)
            // {
            //     if (targetDistance > Vector3.Distance(target.trains[i].transform.position, transform.position))
            //         taretCol = target.trains[i].GetComponent<Collider>();
            // }

            taretCol = target.trains[Random.Range(0, target.trains.Length)].GetComponent<Collider>();
        }
    }

}
