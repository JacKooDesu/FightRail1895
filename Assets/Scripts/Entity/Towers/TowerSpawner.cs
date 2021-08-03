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

        Transform spawnPointParent = default;

        private void OnEnable()
        {
            InputHandler.Singleton.placingTowerEvent.onBegin += () =>
            {
                transform.GetChild(0).gameObject.SetActive(true);

                foreach (GameObject g in GameObject.FindGameObjectsWithTag("TowerSpawnPoint"))
                {
                    g.GetComponent<MeshRenderer>().enabled = true;
                }
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

                if (hit.transform != null && hit.transform.tag == "TowerSpawnPoint")
                {
                    transform.position = hit.transform.position;
                    spawnPointParent = hit.transform;
                }
                else if (hit.point != null)
                {
                    transform.position = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
                    spawnPointParent = null;
                }
                else
                {
                    spawnPointParent = null;
                }

                // should move in InputHandler in future
                if (Input.GetMouseButtonUp(0))
                    InputHandler.Singleton.State = InputHandler.InputState.Normal;
            };

            InputHandler.Singleton.placingTowerEvent.onEnd += () =>
            {
                if (spawnPointParent != null)
                    Spawning();

                transform.GetChild(0).gameObject.SetActive(false);
            };

            InputHandler.Singleton.normalEvent.onBegin += () =>
            {
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("TowerSpawnPoint"))
                {
                    g.GetComponent<MeshRenderer>().enabled = false;
                    // g.SetActive(false);
                }
            };
        }

        void Spawning()
        {
            StartCoroutine(Spawn(SelectTower, spawnPointParent));
        }
    }
}

