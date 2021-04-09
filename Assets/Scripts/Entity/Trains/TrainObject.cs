using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Utils;

namespace JacDev.Entity
{
    public class TrainObject : MonoBehaviour
    {
        public Train train;
        [SerializeField]
        SplineWalker head = default, last = default;
        int index;  // 第幾節車廂
        TrainLine trainLine;

        public float Length
        {    // 火車長度
            get
            {
                return
                Vector3.Distance(head.transform.position, last.transform.position);
            }
        }

        private void Update()
        {
            TestMove();
        }

        void TestMove()
        {
            //transform.Translate(Vector3.forward * Time.deltaTime * train.movementSpeed);
            Quaternion q = Quaternion.Euler((head.transform.eulerAngles + last.transform.eulerAngles)/2);
            transform.SetPositionAndRotation((head.transform.position + last.transform.position) / 2,q);
        }

        public void SetTrain(TrainLine line, int index, float currentLength)
        {
            trainLine = line;
            this.index = index;

            head.spline = trainLine.spline;
            last.spline = trainLine.spline;

            head.CalProgress(currentLength);
            last.CalProgress(currentLength + Length);
        }
    }

}
