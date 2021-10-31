using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    public class TowerSpawnpoint : MonoBehaviour
    {
        public Transform spawnpoint;
        public bool hasTower;

        private void Start()
        {
            if (spawnpoint == null)
                spawnpoint = transform.GetChild(0);
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
    }
}