using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.GameSystem;

namespace JacDev.Entity
{
    public class TowerSpawner : EntitySpawner
    {
        public int SelectTower
        {
            set; get;
        }

        public float zOffset = 15f;
        public float yOffset = 3f;

        private void OnEnable()
        {
            InputHandler.Singleton.placingTowerEvent.onBegin += () =>
            {
                transform.GetChild(0).gameObject.SetActive(true);
            };
            
            InputHandler.Singleton.placingTowerEvent.onUpdate += () =>
            {
                // print(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(
                    new Vector3(
                        Input.mousePosition.x,
                        Input.mousePosition.y,
                        zOffset
                    ));

                // transform.position = new Vector3(screenToWorld.x, yOffset, screenToWorld.z);
                RaycastHit hit;
                Physics.Raycast(
                    Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zOffset)),
                    out hit);

                if (hit.point != null)
                {
                    transform.position = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
                }

                // should move in InputHandler in future
                if (Input.GetMouseButtonUp(0))
                    InputHandler.Singleton.State = InputHandler.InputState.Normal;
            };

            InputHandler.Singleton.placingTowerEvent.onEnd += () =>
            {
                Spawning();
                transform.GetChild(0).gameObject.SetActive(false);
            };
        }

        void Spawning()
        {
            StartCoroutine(Spawn(SelectTower));
        }
    }
}

