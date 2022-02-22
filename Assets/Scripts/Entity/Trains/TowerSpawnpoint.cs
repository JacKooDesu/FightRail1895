using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.GameSystem;
using UnityEngine.EventSystems;

namespace JacDev.Entity
{
    public class TowerSpawnpoint : MonoBehaviour
    {
        public Transform spawnpoint;
        public bool hasTower;

        TowerObject tower = null;

        public Transform rangeSphere;

        private void Start()
        {
            if (spawnpoint == null)
                spawnpoint = transform.GetChild(0);

            rangeSphere.GetComponent<MeshRenderer>().enabled = false;

            System.Action updateAction = () => CheckingTowerData();
            InputHandler.Singleton.selectTowerEvent.onBegin += () =>
            {
                InputHandler.Singleton.selectTowerEvent.onUpdate += updateAction;
            };
            InputHandler.Singleton.selectTowerEvent.onEnd += () =>
            {
                ShowTowerData(false);
                InputHandler.Singleton.selectTowerEvent.onUpdate -= updateAction;
            };
        }

        public void SpawnAreaRendered(bool b)
        {
            if (!hasTower)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = b;
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            }

        }

        public void SpawnOn()
        {
            hasTower = true;

            tower = transform.GetComponentInChildren<EntityObject>() as TowerObject;
            rangeSphere.SetParent(null);
            rangeSphere.localScale = Vector3.one * ((Tower)tower.entitySetting).attackRange * 2;
            rangeSphere.SetParent(transform);
        }

        public void ShowTowerData(bool b)
        {
            if (!hasTower)
                return;

            rangeSphere.GetComponent<MeshRenderer>().enabled = b;
        }

        private void OnMouseDown()
        {
            InputHandler.Singleton.SetState(InputHandler.InputState.SelectTower);
            ShowTowerData(true);

            var orbitCam = Camera.main.GetComponent<OrbitCamera>();
            // var camOriginFocus = orbitCam.Focus;
            var camOriginDistant = orbitCam.Distance;
            // orbitCam.Focus = transform;
            orbitCam.Distance += ((Tower)tower.entitySetting).attackRange * .75f;

            InputHandler.Singleton.selectTowerEvent.onEnd += () =>
            {
                // orbitCam.Focus = camOriginFocus;
                orbitCam.Distance = camOriginDistant;
            };
        }

        void CheckingTowerData()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current == null || !EventSystem.current.IsPointerOverGameObject())
                {
                    InputHandler.Singleton.SetState(InputHandler.InputState.Normal);
                }
            }
        }
    }
}