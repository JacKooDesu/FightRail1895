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
            JacDev.Data.MapData mapData = new Data.MapData();

            for (int i = 0; i < stations.Count - 1; ++i)
            {
                Station s = stations[i];
                s.GUID = System.Guid.NewGuid().ToString();
                mapData.stations1.Add(s);
                mapData.stations2.Add(s);
                mapData.commonStations.Add(s);

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
                        mapData.stations1.Add(new Station("小站", false));
                        mapData.stations2.Add(new Station("小站", false));
                    }
                }
            }
            mapData.stations1.Add(stations[stations.Count - 1]);
            mapData.stations2.Add(stations[stations.Count - 1]);
            mapData.commonStations.Add(stations[stations.Count - 1]);

            // for debug
            /*
            for (int i = 0; i < mapData.path1.Count; ++i)
            {
                if (mapData.path1[i] as Path != null)
                {
                    MonoBehaviour.print($"path object {i}. level is {(mapData.path1[i] as Path).levelSetting.name}");
                }else if(mapData.path1[i] as Station != null){
                    MonoBehaviour.print($"path object {i}. Name is {(mapData.path1[i] as Station).name}");
                }
            }
            */

            return mapData;
        }
    }

    [System.Serializable]
    public class Station
    {
        public string name;
        public string GUID;
        public List<int> sellItemIdList = new List<int>();  // 該站可販售的物資
        public List<int> buyItemIdList = new List<int>();   // 該站可上車的物資

        public bool canFix;
        public bool canUpgradeTrain;
        public bool canUpgradeTower;

        public Station(string name, bool main)
        {
            GUID = System.Guid.NewGuid().ToString();
            this.name = name;
            if (!main)
            {

            }
        }
    }

    [System.Serializable]
    public class Path
    {
        public string GUID;
        public Station s1, s2;

        public Level.LevelSetting levelSetting;
        public int japanese, aboriginal, hakka;

        public Path()
        {
            GUID = System.Guid.NewGuid().ToString();
        }
    }
}
