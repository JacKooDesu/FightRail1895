using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    public class ProjectileSpawner : EntitySpawner
    {
        Vector3 direction;
        public void Launch(Vector3 targetPoint)
        {
            direction = (targetPoint - transform.position).normalized;
            StartCoroutine(Spawn(0));
        }

        protected override IEnumerator Spawn(int index, Transform parent = null)
        {
            SpawnSetting setting = spawnSettings[index];
            GameObject go = Instantiate(setting.entity.prefab);
            EntityObject eo = go.GetComponent<EntityObject>();
            go.transform.position = spawnpoint.position;
            go.transform.SetParent(parent);
            GameHandler.Singleton.entities.Add(eo);

            ((ProjectileObject)eo).Launch(direction);

            yield break;
        }
    }
}
