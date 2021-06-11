using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Entity;

namespace JacDev.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        public LevelSetting levelSetting;
        List<Rect> objectMapping;

        public Transform ground = default;
        public Transform rail = default;

        private void OnEnable()
        {
            for (int j = 0; j < levelSetting.blockCount; ++j)
            {
                // Clear Rect and init the rail
                Setup();
                GameObject parent = transform.Find("Block " + j) ? transform.Find("Block " + j).gameObject : new GameObject("Block " + j);
                parent.transform.SetParent(transform);
                // Generate spawnpoint
                Generate(levelSetting.spawnpoint, parent.transform, levelSetting.spawnpointWidth);
                for (int i = 0; i < levelSetting.mapObjects.Count; ++i)
                {
                    // Generate map object
                    Generate(levelSetting.mapObjects[i], parent.transform);
                }
                parent.transform.localPosition = new Vector3(0, ground.localPosition.y, j * levelSetting.size);
            }

            for (int i = 0; i < (int)(levelSetting.size * levelSetting.blockCount); ++i)
            {
                GameObject temp = Instantiate(levelSetting.rail, rail);
                temp.transform.localPosition = Vector3.forward * ((float)i - levelSetting.size / 2);
            }

            // init ground size
            ground.localScale = new Vector3(
                Mathf.Min(1000, levelSetting.size * levelSetting.blockCount),
                ground.localScale.y,
                levelSetting.size * levelSetting.blockCount);
            // re-position ground
            ground.localPosition = new Vector3(
                ground.localPosition.x,
                ground.localPosition.y,
                ((float)levelSetting.blockCount / 2 - .5f) * levelSetting.size);
        }

        void Setup()
        {
            objectMapping = new List<Rect>();
            objectMapping.Add(new Rect(-levelSetting.railWidth / 2, -levelSetting.size / 2, levelSetting.railWidth, levelSetting.size));

            // foreach (Transform t in transform)
            // {
            //     if (t.GetComponent<EntitySpawner>())
            //     {
            //         Rect r =
            //             new Rect(
            //                 t.localPosition.x - levelSetting.spawnpointAvoidSize / 2,
            //                 t.localPosition.z - levelSetting.spawnpointAvoidSize / 2,
            //                 levelSetting.spawnpointAvoidSize,
            //                 levelSetting.spawnpointAvoidSize / 2 + Mathf.Abs(t.localPosition.z));
            //         objectMapping.Add(r);
            //         print(r.width + "  " + r.height);
            //     }
            // }
        }

        public void Generate(MapObject mo, Transform block, float genAreaX = 0, float genAreaZ = 0)
        {
            // init generate area
            genAreaX = genAreaX == 0 ? levelSetting.size : genAreaX;
            genAreaZ = genAreaZ == 0 ? levelSetting.size : genAreaZ;

            // Clear same type of map object
            Transform parent;
            if (block.Find(mo.name))
            {
                parent = block.Find(mo.name);
            }
            else
            {
                parent = new GameObject(mo.name).transform;
                parent.SetParent(block);
            }
            for (int i = parent.childCount - 1; i >= 0; --i)
            {
                Destroy(parent.GetChild(i).gameObject);
            }

            // init count
            int count = (int)Random.Range(mo.min, (levelSetting.size * levelSetting.size) / (mo.size * mo.size) * mo.res);
            count = (count > mo.max ? mo.max : count);

            // placing map object
            for (int i = 0; i < count; ++i)
            {
                float x = Random.Range(-(genAreaX / 2) + mo.size, (genAreaX / 2) - mo.size);
                float z = Random.Range(-(genAreaZ / 2) + mo.size, (genAreaZ / 2) - mo.size);
                Rect rect = new Rect(x + mo.size / 2, z + mo.size / 2, mo.size, mo.size);
                bool overlap = false;
                for (int j = 0; j < objectMapping.Count; ++j)
                {
                    if (rect.Overlaps(objectMapping[j]))
                    {
                        // print("overlap");
                        // break if overlap
                        overlap = true;
                        break;
                    }
                }

                if (overlap)
                {
                    continue;
                }
                else
                {
                    objectMapping.Add(rect);
                    GameObject g = Instantiate(mo.origins[Random.Range(0, mo.origins.Length)]);
                    g.transform.SetParent(parent);
                    g.transform.localPosition = new Vector3(x, 0, z);
                    g.transform.eulerAngles = new Vector3(0, Random.Range(0, 360f));
                }
            }
        }

        public void DebugButton()
        {
            OnEnable();
        }
    }
}

