using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Utils;

namespace JacDev.Entity
{
    public class TrainLine : MonoBehaviour
    {
        [SerializeField]
        TrainObject[] trains;
        public BezierSpline spline; // 路線

        private void Start()
        {
            float totalLength = 0f;
            for (int i = 0; i < trains.Length; ++i)
            {
                trains[i].SetTrain(this, i, totalLength);
                print(totalLength += trains[i].Length);
            }
        }
    }
}

