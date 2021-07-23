using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Utils;

namespace JacDev.Entity
{
    public class TrainObject : EntityObject
    {
        public Transform front, back;

        // 20210611 fixed no longer use spline walker to move the train
        //SplineWalker head = default, last = default;
        // public void SetTrain(TrainLine line, int index, float currentLength)
        // {
        //     trainLine = line;
        //     this.index = index;

        //     head.spline = trainLine.spline;
        //     last.spline = trainLine.spline;

        //     head.CalProgress(currentLength + Length);
        //     last.CalProgress(currentLength);
        // }


        int index;  // 第幾節車廂
        TrainLine trainLine;

        public float Length
        {    // 火車長度
            get
            {
                return
                Vector3.Distance(front.transform.position, back.transform.position);
            }
        }

        private void Update()
        {
            //TestMove();
        }

        // void TestMove()
        // {
        //     transform.Translate(Vector3.forward * Time.deltaTime * trainSpeed);
        //     // Quaternion q = Quaternion.Euler((front.transform.eulerAngles + back.transform.eulerAngles) / 2);
        //     // transform.SetPositionAndRotation((front.transform.position + back.transform.position) / 2, q);
        // }
    }

}
