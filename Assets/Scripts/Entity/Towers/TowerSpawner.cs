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
                if (GameHandler.Singleton.money < (spawnSettings[SelectTower].entity as Tower).price)
                {
                    InputHandler.Singleton.State = InputHandler.InputState.Normal;
                    return;
                }

                transform.GetChild(0).gameObject.SetActive(true);

                // foreach (GameObject g in GameObject.FindGameObjectsWithTag("TowerSpawnPoint"))
                foreach (TowerSpawnpoint g in GameObject.FindObjectsOfType<TowerSpawnpoint>())
                {
                    // g.GetComponent<MeshRenderer>().enabled = true;
                    g.GetComponent<TowerSpawnpoint>().SpawnAreaRendered(true);
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

                // if (hit.transform != null && hit.transform.tag == "TowerSpawnPoint")
                if (hit.transform != null && hit.transform.GetComponent<TowerSpawnpoint>() != null)
                {
                    // transform.position = hit.transform.position;
                    transform.position = hit.transform.GetComponent<TowerSpawnpoint>().spawnpoint.position;
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
                {
                    Spawning();
                    GameHandler.Singleton.money -= (spawnSettings[SelectTower].entity as Tower).price;
                }


                transform.GetChild(0).gameObject.SetActive(false);
            };

            InputHandler.Singleton.normalEvent.onBegin += () =>
            {
                // foreach (GameObject g in GameObject.FindGameObjectsWithTag("TowerSpawnPoint"))
                foreach (TowerSpawnpoint g in GameObject.FindObjectsOfType<TowerSpawnpoint>())
                {
                    // g.GetComponent<MeshRenderer>().enabled = false;
                    g.GetComponent<TowerSpawnpoint>().SpawnAreaRendered(false);
                    // g.SetActive(false);
                }
            };
        }

        void Spawning()
        {
            StartCoroutine(Spawn(SelectTower, spawnPointParent));

            // 2021.10.31 added
            spawnPointParent.GetComponent<TowerSpawnpoint>().SpawnOn();
        }
    }
}

