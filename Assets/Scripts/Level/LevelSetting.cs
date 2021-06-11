using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JacDev.Level
{
    // Copy from Test.GenerateTrees
    [System.Serializable, CreateAssetMenu(fileName = "Level", menuName = "JacDev/Create Level", order = 1)]
    public class LevelSetting : ScriptableObject
    {
        public List<MapObject> mapObjects = new List<MapObject>();

        public float size = 10f;

        [Header("Avoid Setting")]
        public float railWidth = 5f;
        public float spawnpointAvoidSize = 5f;

        [Header("Main Level Setting")]
        public int blockCount = 10;

        public MapObject spawnpoint;
        public float spawnpointWidth = 10f;

        public GameObject rail;
    }

    [System.Serializable]
    public class MapObject
    {
        public string name;
        public GameObject[] origins;
        public float size = 2f;
        public int max = 20;
        public int min = 5;
        [Range(0f, 1f)]
        public float res = .5f;

        public MapObject(string name, GameObject[] origins, float size, int max, int min, float res)
        {
            this.name = name;
            this.origins = origins;
            this.size = size;
            this.max = max;
            this.min = min;
            this.res = res;
        }
    }
}
