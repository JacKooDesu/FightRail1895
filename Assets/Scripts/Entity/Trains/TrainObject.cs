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
                Vector3.Distance(head.transform.localPosition, last.transform.localPosition);
            }
        }

        private void Update()
        {
            TestMove();
        }

        void TestMove()
        {
            // transform.Translate(Vector3.forward * Time.deltaTime * train.movementSpeed);
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
