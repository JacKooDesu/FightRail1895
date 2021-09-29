using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Map
{
    [CreateAssetMenu(fileName = "Map Setting", menuName = "JacDev/Map Setting", order = 0)]
    public class MapSetting : ScriptableObject
    {
        [Header("地圖設定")]
        public List<Level.LevelSetting> mountainLevels = new List<Level.LevelSetting>();
        public List<Level.LevelSetting> oceanLevels = new List<Level.LevelSetting>();

        [Header("大站設定")]
        public List<Station> stations = new List<Station>();
        public int minSpotCount, maxSpotCount;  // 兩大站之間小站數量

        public Data.MapData InitMap()
        {
            GameObject g = new GameObject("MapData");
            JacDev.Data.MapData mapData = g.AddComponent<Data.MapData>();

            for (int i = 0; i < stations.Count - 1; ++i)
            {
                mapData.path1.Add(stations[i]);
                mapData.path2.Add(stations[i]);
                mapData.commonStations.Add(stations[i]);

                int spotCount = Random.Range(minSpotCount, maxSpotCount + 1);

                for (int j = 0; j < spotCount; ++j)
                {
                    Path p1 = new Path();
                    p1.levelSetting = mountainLevels[Random.Range(0, mountainLevels.Count)];
                    mapData.path1.Add(p1);

                    Path p2 = new Path();
                    p2.levelSetting = oceanLevels[Random.Range(0, oceanLevels.Count)];
                    mapData.path2.Add(p2);

                    if (j != spotCount - 1)
                    {
                        mapData.path1.Add(new Station("小站", false));
                        mapData.path2.Add(new Station("小站", false));
                    }
                }
            }
            mapData.commonStations.Add(stations[stations.Count - 1]);

            for (int i = 0; i < mapData.path1.Count; ++i)
            {
                if (mapData.path1[i] as Path != null)
                {
                    MonoBehaviour.print($"path object {i}. level is {(mapData.path1[i] as Path).levelSetting.name}");
                }else if(mapData.path1[i] as Station != null){
                    MonoBehaviour.print($"path object {i}. Name is {(mapData.path1[i] as Station).name}");
                }
            }

            return mapData;
        }
    }

    [System.Serializable]
    public class Station
    {
        public string name;
        public List<int> sellItemIdList = new List<int>();  // 該站可販售的物資
        public List<int> buyItemIdList = new List<int>();   // 該站可上車的物資

        public bool canFix;
        public bool canUpgradeTrain;
        public bool canUpgradeTower;

        public Station(string name, bool main)
        {
            this.name = name;
            if (!main)
            {

            }
        }
    }

    [System.Serializable]
    public class Path
    {
        public Station s1, s2;

        public Level.LevelSetting levelSetting;
        public int japanese, aboriginal, hakka;

        public Path()
        {

        }
    }
}
