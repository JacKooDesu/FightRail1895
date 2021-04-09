using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Utils;

namespace JacDev.Entity
{
    public class TrainLine : MonoBehaviour
    {
        public TrainObject[] trains;
        public BezierSpline spline; // 路線

        private void Start()
        {
            float totalLength = 0f;
            for (int i = trains.Length - 1; i >= 0; --i)
            {
                trains[i].SetTrain(this, i, totalLength);
                print(totalLength += trains[i].Length);
            }
        }
    }
}

