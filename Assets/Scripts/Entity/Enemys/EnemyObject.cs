using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    public class EnemyObject : MonoBehaviour
    {
        public Enemy enemy;

        private void Update() {
            TestMove();
        }
        
        public void TestMove()
        {
            transform.Translate(enemy.movementSpeed * Vector3.forward * Time.deltaTime);
        }

        public void SetEnemy(Enemy e)
        {
            enemy = e;
        }
    }

}
