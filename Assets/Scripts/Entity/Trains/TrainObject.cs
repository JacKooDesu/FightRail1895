using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    public class TrainObject : MonoBehaviour
    {
        public Train train;

        private void Update() {
            TestMove();
        }

        void TestMove(){
            transform.Translate(Vector3.forward * Time.deltaTime * train.movementSpeed);
        }
        
    }

}
