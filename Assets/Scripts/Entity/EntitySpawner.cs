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

        [SerializeField]
        SpawnSetting[] spawnSettings;
        
        // protected IEnumerator Spawn()
        // {
        //     int hasSpawn = 0;
        //     float tempInterval = 0f;

        //     while (hasSpawn < amount)
        //     {
        //         if (spawnType == SpawnType.Once)
        //         {
        //             GameObject go = new GameObject();
        //             EntityObject eo = go.AddComponent(entity.EntityObject());
        //             go.transform.position = spawnpoint.position;
        //             GameHandler.Singleton.entities.Add(entityObject);
        //             continue;
        //         }

        //     }
        //     yield return null;
        // }
    }
}

