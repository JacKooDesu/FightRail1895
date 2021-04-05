using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    public class EnemyObject : MonoBehaviour
    {
        public Enemy enemy;
        public TrainObject target;

        private void Update()
        {
            TestMove();
        }

        public void TestMove()
        {
            // transform.Translate(enemy.movementSpeed * Vector3.forward * Time.deltaTime);
            if (target != null)
            {
                Collider taretCol = target.GetComponent<Collider>();

                if ((taretCol.ClosestPoint(transform.position) - GetComponent<Collider>().ClosestPoint(target.transform.position)).magnitude >= 1f)
                    transform.Translate(enemy.movementSpeed * Vector3.forward * Time.deltaTime);


                transform.LookAt(taretCol.ClosestPoint(transform.position));

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
    }

}
