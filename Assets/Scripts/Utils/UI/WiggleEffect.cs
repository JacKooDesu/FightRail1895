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
            float x = (Mathf.PerlinNoise(Time.time, 0) - .5f) * xAmount;
            float y = (Mathf.PerlinNoise(0, Time.time) - .5f) * yAmount;
            transform.localPosition = new Vector3(x, y, 0);
        }
    }

}
