using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Utils;

namespace JacDev.Entity
{
    public class TrainLine : MonoBehaviour
    {
        public TrainObject[] trains;

        public float speed; // should be static?

        KeyCode[] switchKey = {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,
            KeyCode.Alpha0,
        };

        private void Start()
        {
            float lengthTemp = 0;

            for (int i = 0; i < trains.Length; ++i)
            {
                print(lengthTemp);
                trains[i].transform.localPosition = new Vector3(0, 0, lengthTemp);
                lengthTemp -= trains[i].Length;

            }

            EnemyObject.target = this;
        }

        private void Update()
        {
            TestMove();
            SwitchFocusTrain();
        }

        void TestMove()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        void SwitchFocusTrain()
        {
            for (int i = 0; i < trains.Length; ++i)
            {
                if (Input.GetKeyDown(switchKey[i]))
                {
                    Camera.main.GetComponent<OrbitCamera>().SetFocus(trains[i].transform);
                }
            }
        }
    }
}

