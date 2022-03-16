using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace JacDev.Utils
{
    public class WiggleEffect : MonoBehaviour
    {
        public float xAmount, yAmount;
        public float speed;

        private void Update()
        {
            float x = Mathf.PerlinNoise(Time.time, 0) * xAmount;
            float y = Mathf.PerlinNoise(0, Time.time) * yAmount;
            transform.localPosition = new Vector3(x, y, 0);
        }
    }

}
