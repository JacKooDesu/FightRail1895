using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Entity;

namespace JacDev.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        static LevelGenerator singleton = null;

        public static LevelGenerator Singleton
        {
            get
            {
                singleton = FindObjectOfType(typeof(LevelGenerator)) as LevelGenerator;

                if (singleton == null)
                {
                    GameObject g = new GameObject("LevelGenerator");
                    singleton = g.AddComponent<LevelGenerator>();
                }

                return singleton;
            }
        }

        public enum GenerateType
        {
            General,
            Constant
        }

        public GenerateType generateType;

        public LevelSetting levelSetting;
        List<Rect> objectMapping;
        List<GameObject> blocks = new List<GameObject>();

        public Transform ground = default;
        public TerrainGenerator terrainGenerator = default;
        public Transform rail = default;

        public float totalLength;

        public MapObject fromMapObject, destMapObject;  // 車站地圖物件
        [HideInInspector]
        public Transform dest;  // 目的地

        private void OnEnable()
        {
            // BuildMap();
        }

        public void BindSetting()
        {
            DataManager dm = DataManager.Singleton;

            MapObject from = dm.GetMapData().FindStation(dm.PlayerData.currentStation).stationObject;
            MapObject dest = dm.GetMapData().FindStation(dm.PlayerData.nextStation).stationObject;
            fromMapObject = from.origins.Length == 0 ? SettingManager.Singleton.MapSetting.subStationObject : from;
            destMapObject = dest.origins.Length == 0 ? SettingManager.Singleton.MapSetting.subStationObject : dest;

            // ground.GetComponent<MeshRenderer>().sharedMaterial = levelSetting.groundMaterial;

        }

        public void BuildMap()
        {
            BindSetting();

            for (int i = 0; i < levelSetting.blockCount; ++i)
            {
                Transform t = Instantiate(ground, transform);
                t.localPosition += Vector3.forward * levelSetting.size * i;
                t.localScale = new Vector3(levelSetting.size, 1, levelSetting.size);
                t.GetComponent<MeshRenderer>().sharedMaterial = levelSetting.groundMaterial;
            }
            ground.gameObject.SetActive(false);

            // init ground size
            terrainGenerator.setting = levelSetting.terrainSetting;
            terrainGenerator.size = new Vector2Int((int)(levelSetting.size), (int)(levelSetting.size * levelSetting.blockCount));
            terrainGenerator.resolution = new Vector2Int(100, 100 * levelSetting.blockCount);
            terrainGenerator.InitTerrain();
            terrainGenerator.transform.localPosition += Vector3.forward * levelSetting.size;

            totalLength = (float)(levelSetting.blockCount - 1) * levelSetting.size;

            // init mapOjbect
            for (int j = 0; j < levelSetting.blockCount; ++j)
            {
                // Clear Rect and init the rail
                Setup();
                GameObject parent = transform.Find("Block " + j) ? transform.Find("Block " + j).gameObject : new GameObject("Block " + j);
                parent.transform.SetParent(transform);

                // easy to manage the block
                blocks.Add(parent);

                // Generate spawnpoint,except first and last
                if (generateType == GenerateType.General)
                {
                    if (j == 0)
                    {
                        // 2021.10.31 added
                        Rect rect = new Rect(levelSetting.stationOffset.x + fromMapObject.size / 2, levelSetting.stationOffset.y + fromMapObject.size / 2, fromMapObject.size, fromMapObject.size);
                        objectMapping.Add(rect);
                        GameObject g = Instantiate(fromMapObject.origins[0]);
                        g.transform.SetParent(parent.transform);
                        g.transform.localPosition = new Vector3(levelSetting.stationOffset.x, 0, levelSetting.stationOffset.y);

                        // Rect rect = new Rect(levelSetting.stationOffset.x + levelSetting.from.size / 2, levelSetting.stationOffset.y + levelSetting.from.size / 2, levelSetting.from.size, levelSetting.from.size);
                        // objectMapping.Add(rect);
                        // GameObject g = Instantiate(levelSetting.from.origins[0]);
                        // g.transform.SetParent(parent.transform);
                        // g.transform.localPosition = new Vector3(levelSetting.stationOffset.x, 0, levelSetting.stationOffset.y);
                    }
                    else if (j == levelSetting.blockCount - 1)
                    {
                        // 2021.10.31 added
                        Rect rect = new Rect(levelSetting.stationOffset.x + destMapObject.size / 2, levelSetting.stationOffset.y + destMapObject.size / 2, destMapObject.size, destMapObject.size);
                        objectMapping.Add(rect);
                        GameObject g = Instantiate(destMapObject.origins[0]);
                        g.transform.SetParent(parent.transform);
                        g.transform.localPosition = new Vector3(levelSetting.stationOffset.x, 0, levelSetting.stationOffset.y);
                        // need assign dest for map
                        dest = g.transform;

                        // Rect rect = new Rect(levelSetting.stationOffset.x + levelSetting.dest.size / 2, levelSetting.stationOffset.y + levelSetting.dest.size / 2, levelSetting.dest.size, levelSetting.dest.size);
                        // objectMapping.Add(rect);
                        // GameObject g = Instantiate(levelSetting.dest.origins[0]);
                        // g.transform.SetParent(parent.transform);
                        // g.transform.localPosition = new Vector3(levelSetting.stationOffset.x, 0, levelSetting.stationOffset.y);
                        // // need assign dest for map
                        // dest = g.transform;
                    }
                    else
                    {

                        Generate(levelSetting.spawnpoint, parent.transform, levelSetting.spawnpointWidth);
                    }
                }
                else
                {
                    if (levelSetting.spawnpoint.origins.Length != 0)
                        Generate(levelSetting.spawnpoint, parent.transform, levelSetting.spawnpointWidth);
                }

                for (int i = 0; i < levelSetting.mapObjects.Count; ++i)
                {
                    // Generate map object
                    Generate(levelSetting.mapObjects[i], parent.transform);
                }
                parent.transform.localPosition = new Vector3(0, ground.localPosition.y, j * levelSetting.size);
            }

            // init rail
            for (int i = 0; i < (int)(levelSetting.size * levelSetting.blockCount / levelSetting.railLength); ++i)
            {
                GameObject temp = Instantiate(levelSetting.rail, rail);
                temp.transform.localPosition = Vector3.forward * ((float)i * levelSetting.railLength - levelSetting.size / 2);
            }
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

        private void Update()
        {
            // need be modify in future
            if (generateType == GenerateType.Constant)
            {
                if (TrainLine.hasMove / levelSetting.size > (float)levelSetting.blockCount / 2)
                {
                    blocks[0].transform.localPosition = blocks[blocks.Count - 1].transform.localPosition + levelSetting.size * Vector3.forward;
                    blocks.Add(blocks[0]);
                    blocks.RemoveAt(0);
                    ground.localPosition += levelSetting.size * Vector3.forward;
                    TrainLine.hasMove -= levelSetting.size;
                }
            }
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
                float x = Random.Range((mo.leftSide ? -(genAreaX / 2) + mo.size : 0), (mo.rightSide ? (genAreaX / 2) - mo.size : 0));
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
                    g.transform.localPosition = (new Vector3(x, 0, z) + mo.positionOffset);
                    // g.transform.localPosition += Vector3.up * ground.GetComponent<TerrainGenerator>().GetHeight(new Vector2(x + mo.size, z + mo.size));
                    g.transform.eulerAngles = new Vector3(0, Random.Range(0, 360f));
                }
            }
        }

        public void DebugButton()
        {
            BuildMap();
        }
    }
}

