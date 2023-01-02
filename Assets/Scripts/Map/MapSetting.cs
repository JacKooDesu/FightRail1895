using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Map
{
    [CreateAssetMenu(fileName = "Map Setting", menuName = "JacDev/Map/Create Map Setting", order = 0)]
    public class MapSetting : ScriptableObject
    {
        [Header("地圖設定")]
        public List<Level.LevelSetting> mountainLevels = new List<Level.LevelSetting>();
        public List<Level.LevelSetting> oceanLevels = new List<Level.LevelSetting>();

        [Header("大站設定")]
        public List<StationSetting> stationSettings = new List<StationSetting>();
        // public List<Station> stations = new List<Station>();
        public int minSpotCount, maxSpotCount;  // 兩大站之間小站數量

        [Header("小站設定")]
        public Level.MapObject subStationObject;

        [Header("販售物資設定")]
        public int tradeCount;  // 交易物品種類數量
        public float minSellPriceMultiplyPerStation, maxSellPriceMultiplyPerStation;    // 距離影響初始價格
        public int minSellStationDistant, maxSellStationDistant;    // 物資販售與收購最大距離

        public Data.MapData InitMap()
        {
            // 綁定大站
            List<Station> stations = new List<Station>();
            foreach (StationSetting ss in stationSettings)
            {
                ss.station.ResetItems();
                stations.Add(ss.station);
            }


            // 綁定所有站點販售與收購的物品
            // int totalTradeCount = tradeCount * stations.Count;

            int currentItemCount = 1;
            foreach (Data.ItemPriceData.PriceSetting ps in DataManager.Singleton.ItemPriceData.priceSettings)
            {
                // bool settingSell = Random.Range(0f, 1f) >= .5f;
                bool settingSell = true;    // 得先有得賣才有得買嗎?
                int interval = Random.Range(minSellStationDistant, maxSellStationDistant + 1);  // 物品大站間隔
                int total = Random.Range(2, stations.Count / (interval + 1));   // 決定販售站點數量
                UnityEngine.Debug.Log($"總共:{total} / 間隔:{interval}");

                // 盡量讓所有站點都有販售物資
                List<int> stationIndexList = new List<int>();
                int sellMax = 1;
                int iter1 = 0;
                while (stationIndexList.Count < currentItemCount)
                {
                    iter1++;
                    for (int s = 0; s < stations.Count; s++)
                    {
                        if (stations[s].sellItemIdList.Count <= sellMax)
                            stationIndexList.Add(s);
                    }

                    if (iter1 >= 1000) { UnityEngine.Debug.Log("ERROR"); break; }
                }

                int from = stationIndexList[Random.Range(0, stationIndexList.Count)];

                int count = 0;
                int breakCount = 0, iter = 0;
                int breaker = 1000;
                while (count < total)
                {
                    int current = from + count * interval;
                    if (current >= stations.Count || current < 0)
                    {
                        if (iter == 1)
                            break;

                        interval *= -1;
                        count = 1;
                        current = from + count * interval;
                        iter++;
                    }

                    if (settingSell)
                        stations[current].sellItemIdList.Add(ps.id);
                    else
                        stations[current].buyItemIdList.Add(ps.id);
                    count++;

                    breakCount++;
                    if (breakCount >= breaker)
                        total--;

                    settingSell = !settingSell;
                }

                currentItemCount++;
            }

            // 生成小站與路徑
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
        [HideInInspector] public string GUID;
        public Level.MapObject stationObject;
        // public JacDev.Shop.ShopSetting shopsetting;
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

        public void ResetItems()
        {
            sellItemIdList = new List<int>();
            buyItemIdList = new List<int>();
        }
    }

    [System.Serializable]
    public class Path
    {
        [HideInInspector] public string GUID;
        public Station s1, s2;

        public Level.LevelSetting levelSetting;
        public int japanese, aboriginal, hakka;

        public Path()
        {
            GUID = System.Guid.NewGuid().ToString();
        }
    }
}
