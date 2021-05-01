using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    public abstract class EntitySpawner : MonoBehaviour
    {
        [SerializeField, Header("目標生成物")]
        protected EntitySetting entity = default;

        [SerializeField, Header("生成點")]
        protected Transform spawnpoint = default;

        public enum SpawnType
        {
            Once,
            OneByOne
        }
        [SerializeField, Header("模式")]
        protected SpawnType spawnType = default;

        [SerializeField, Header("數量")]
        protected int amount = 1;

        [SerializeField, Header("生成間格")]
        protected float interval;
        protected float Interval
        {
            get
            {
                if (spawnType == SpawnType.Once)
                    return 0;
                else
                    return interval;
            }
        }

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

