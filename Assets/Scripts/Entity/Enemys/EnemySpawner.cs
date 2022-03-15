using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    public class EnemySpawner : EntitySpawner
    {
        [SerializeField]
        protected float radius = 30f;
        bool active = true;

        private void Update()
        {
            if (active)
            {
                foreach (RaycastHit hit in Physics.SphereCastAll(
                    spawnpoint.position, radius, Vector3.forward, 0f))
                {
                    if (hit.transform.GetComponent<TrainObject>())
                    {
                        StartCoroutine(Spawn(0));
                        active = false;
                    }
                }
            }
        }

        protected override IEnumerator Spawn(int index, Transform parent = default)
        {
            int hasSpawn = 0;
            float tempInterval = 0f;
            SpawnSetting setting = spawnSettings[index];

            while (hasSpawn < setting.amount)
            {
                if (setting.spawnType == SpawnSetting.SpawnType.Once)
                {
                    GameObject go = Instantiate(setting.entity.prefab);
                    EntityObject eo = go.GetComponent<EntityObject>();
                    eo.Init();
                    go.transform.position = spawnpoint.position;
                    go.transform.SetParent(parent);
                    GameHandler.Singleton.entities.Add(eo);
                    hasSpawn += 1;
                }
                else
                {
                    if (tempInterval < setting.interval)
                    {
                        tempInterval += Time.deltaTime;
                        yield return null;
                    }
                    else
                    {
                        GameObject go = Instantiate(setting.entity.prefab);
                        EntityObject eo = go.GetComponent<EntityObject>();
                        eo.Init();

                        // for pre build
                        var offset = new Vector3(Random.Range(1f,-.1f),0,Random.Range(1f,-.1f));

                        go.transform.position = spawnpoint.position + offset;
                        go.transform.SetParent(parent);
                        GameHandler.Singleton.entities.Add(eo);
                        hasSpawn += 1;
                        tempInterval = 0;
                    }
                }
            }
            yield break;
        }
    }
}

