using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Level;

namespace JacDev.Entity
{
    public class TrainLine : MonoBehaviour
    {
        public TrainObject[] trains;

        public float speed; // should be static?
        public static float hasMove = 0;
        bool finished = false;

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
            hasMove += Time.deltaTime * speed;

            if (hasMove >= LevelGenerator.Singleton.totalLength && !finished)
            {
                Camera.main.GetComponent<OrbitCamera>().SetFocus(LevelGenerator.Singleton.dest);
                AsyncSceneLoader.Singleton.LoadScene("ShopScene", 2f);
                finished = true;
            }
        }

        void SwitchFocusTrain()
        {
            for (int i = 0; i < trains.Length; ++i)
            {
                if (Input.GetKeyDown(switchKey[i]))
                {
                    if (Camera.main.GetComponent<OrbitCamera>())
                        Camera.main.GetComponent<OrbitCamera>().SetFocus(trains[i].transform);
                }
            }
        }
    }
}

