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

                transform.Translate(enemy.movementSpeed * Vector3.forward * Time.deltaTime);

                transform.LookAt(taretCol.ClosestPoint(transform.position));
            }


        }

        public void SetEnemy(Enemy e)
        {
            enemy = e;
        }
    }

}
