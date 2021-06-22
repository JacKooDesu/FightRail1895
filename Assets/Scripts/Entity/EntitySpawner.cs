using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    public class EntitySpawner : MonoBehaviour
    {
        [SerializeField, Header("生成點")]
        protected Transform spawnpoint = default;

        [System.Serializable]
        public class SpawnSetting
        {
            [SerializeField, Header("目標生成物")]
            public EntitySetting entity = default;

            public enum SpawnType
            {
                Once,
                OneByOne
            }
            [SerializeField, Header("模式")]
            public SpawnType spawnType = default;

            [SerializeField, Header("數量")]
            public int amount = 1;

            [SerializeField, Header("生成間格")]
            public float interval;
            public float Interval
            {
                get
                {
                    if (spawnType == SpawnType.Once)
                        return 0;
                    else
                        return interval;
                }
            }
        }

        public void DebugSpawn(int index = 0)
        {
            StartCoroutine(Spawn(index));
        }

        [SerializeField]
        SpawnSetting[] spawnSettings;

        protected IEnumerator Spawn(int index, Transform parent = default)
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
                        go.transform.position = spawnpoint.position;
                        go.transform.SetParent(parent);
                        GameHandler.Singleton.entities.Add(eo);
                        hasSpawn += 1;
                        tempInterval = 0;
                    }
                }
            }
            yield return null;
        }
    }
}

