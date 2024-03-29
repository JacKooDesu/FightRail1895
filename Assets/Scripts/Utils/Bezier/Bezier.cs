﻿using UnityEngine;

namespace JacDev.Utils
{
    public static class Bezier
    {
        // 二次公式
        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;

            return
                oneMinusT * oneMinusT * p0 +
                2f * oneMinusT * t * p1 +
                t * t * p2;

            // return Vector3.Lerp(
            //     Vector3.Lerp(p0, p1, t), 
            //     Vector3.Lerp(p1, p2, t), 
            //     t);
        }

        // 得出導數
        public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            return
                2f * (1f - t) * (p1 - p0) +
                2f * t * (p2 - p1);
        }


        // 三方公式
        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return
                oneMinusT * oneMinusT * oneMinusT * p0 +
                3f * oneMinusT * oneMinusT * t * p1 +
                3f * oneMinusT * t * t * p2 +
                t * t * t * p3;
        }

        // 求導數
        public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return
                3f * oneMinusT * oneMinusT * (p1 - p0) +
                6f * oneMinusT * t * (p2 - p1) +
                3f * t * t * (p3 - p2);
        }

        public static float GetLength(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float chord = Vector3.Distance(p3, p0);
            float chordNet =
                Vector3.Distance(p0, p1) +
                Vector3.Distance(p1, p2) +
                Vector3.Distance(p2, p3);

            return (chord + chordNet) / 2;
        }
    }

}
