using UnityEngine;

namespace JacDev.Utils
{
    public class SplineWalker : MonoBehaviour
    {
        public BezierSpline spline;
        public float speed;  // 速度
        [SerializeField]
        float progress;

        public bool lookForward = true;

        public enum SplineWalkerMode
        {
            Once,
            Loop,
            PingPong
        }
        public SplineWalkerMode mode;
        bool goingForward = true;   // PingPong 模式倒車


        private void Update()
        {
            if (goingForward)
            {
                progress += Time.deltaTime * speed / spline.GetVelocity(progress).magnitude;
                if (progress > 1f)
                {
                    switch (mode)
                    {
                        case SplineWalkerMode.Once:
                            progress = 1f;
                            break;

                        case SplineWalkerMode.Loop:
                            progress -= 1f; // 接回原點
                            break;

                        case SplineWalkerMode.PingPong:
                            progress = 2f - progress;
                            goingForward = false;
                            break;
                    }
                }
            }
            else
            {
                progress -= Time.deltaTime * speed;
                if (progress < 0f)
                {
                    progress = -progress;   // 回彈
                    goingForward = true;
                }
            }

            Vector3 pos = spline.GetPoint(progress);
            transform.position = pos;

            if (lookForward)
            {
                transform.LookAt(pos + spline.GetDirection(progress));
            }
        }

        public void CalProgress(float length)
        {
            progress = length / spline.GetLength();
        }
    }

}
