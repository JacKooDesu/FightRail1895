using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace JacDev.UI.ShopScene
{
    public class MapCanvas : MonoBehaviour
    {
        public GameObject linePrefab;
        public GameObject mainStationIconPrefab;
        public GameObject subStationIconPrefab;

        public float branchPosOffset;
        public float pathLength;

        public Transform mapParent;

        [SerializeField] List<RectTransform> stationObjects1 = new List<RectTransform>();
        [SerializeField] List<RectTransform> stationObjects2 = new List<RectTransform>();

        private void Start()
        {
            InitMap();
            BindSelectableStation(DataManager.Singleton.GetMapData(false).FindStation(DataManager.Singleton.PlayerData.currentStation));
        }

        public void InitMap()
        {
            Data.MapData map = DataManager.Singleton.GetMapData();

            for (int i = 0; i < map.stations1.Count; ++i)
            {
                Map.Station s = map.stations1[i];
                GameObject stationIcon;
                if (map.commonStations.FindIndex(cs => s.GUID == cs.GUID) != -1)    // 是大站
                {
                    stationIcon = Instantiate(mainStationIconPrefab, mapParent);
                    stationIcon.GetComponentInChildren<Text>().text = map.stations1[i].name;
                    (stationIcon.transform as RectTransform).anchoredPosition = new Vector2(i * pathLength, 0);
                }
                else
                {
                    stationIcon = Instantiate(subStationIconPrefab, mapParent);
                    (stationIcon.transform as RectTransform).anchoredPosition = new Vector2(i * pathLength, branchPosOffset);
                }

                stationIcon.GetComponent<JacDev.Utils.TooltipSystem.TooltipTrigger>().content = map.stations1[i].name;

                stationObjects1.Add(stationIcon.transform as RectTransform);
            }

            for (int i = 0; i < map.stations2.Count; ++i)
            {
                Map.Station s = map.stations2[i];
                GameObject stationIcon;
                if (map.commonStations.FindIndex(cs => s.GUID == cs.GUID) != -1)    // 是大站
                {
                    stationObjects2.Add(stationObjects1[i]);
                    continue;
                }
                else
                {
                    stationIcon = Instantiate(subStationIconPrefab, mapParent);
                    (stationIcon.transform as RectTransform).anchoredPosition = new Vector2(i * pathLength, -branchPosOffset);
                }

                stationIcon.GetComponent<JacDev.Utils.TooltipSystem.TooltipTrigger>().content = map.stations2[i].name;

                stationObjects2.Add(stationIcon.transform as RectTransform);
            }


            float totalWidth = stationObjects1[stationObjects1.Count - 1].anchoredPosition.x - stationObjects1[0].anchoredPosition.x + stationObjects1[0].sizeDelta.x;

            (mapParent.transform as RectTransform).sizeDelta = new Vector2(totalWidth, (mapParent.transform as RectTransform).sizeDelta.y);

            // path 1
            for (int i = 0; i < stationObjects1.Count - 1; ++i)
            {
                RectTransform pathObject = (Instantiate(linePrefab, mapParent).transform as RectTransform);
                RectTransform nextStation = stationObjects1[i + 1];
                RectTransform lastStation = stationObjects1[i];

                Vector2 distant = nextStation.anchoredPosition - lastStation.anchoredPosition;

                float zRotationOffset = Vector2.SignedAngle(distant, Vector2.right);

                pathObject.sizeDelta = new Vector2(distant.magnitude - nextStation.sizeDelta.x / 2 - lastStation.sizeDelta.x / 2, pathObject.sizeDelta.y);
                // pathObject.anchoredPosition = (lastStation.anchoredPosition + nextStation.anchoredPosition) * .5f;
                pathObject.anchoredPosition = (lastStation.anchoredPosition + nextStation.anchoredPosition) * .5f;
                pathObject.eulerAngles = Vector3.back * zRotationOffset;
            }

            // path 2
            for (int i = 0; i < stationObjects2.Count - 1; ++i)
            {
                RectTransform pathObject = (Instantiate(linePrefab, mapParent).transform as RectTransform);
                RectTransform nextStation = stationObjects2[i + 1];
                RectTransform lastStation = stationObjects2[i];

                Vector2 distant = nextStation.anchoredPosition - lastStation.anchoredPosition;

                float zRotationOffset = Vector2.SignedAngle(distant, Vector2.right);

                pathObject.sizeDelta = new Vector2(distant.magnitude - nextStation.sizeDelta.x / 2 - lastStation.sizeDelta.x / 2, pathObject.sizeDelta.y);
                // pathObject.anchoredPosition = (lastStation.anchoredPosition + nextStation.anchoredPosition) * .5f;
                pathObject.anchoredPosition = (lastStation.anchoredPosition + nextStation.anchoredPosition) * .5f;
                pathObject.eulerAngles = Vector3.back * zRotationOffset;
            }

            // fix position
            foreach (RectTransform rt in mapParent)
            {
                rt.anchoredPosition += Vector2.left * ((totalWidth - stationObjects1[0].sizeDelta.x) / 2);
            }
        }

        public void BindSelectableStation(Map.Station currentStation)
        {
            Data.MapData map = DataManager.Singleton.GetMapData();

            if (currentStation == null)
                currentStation = map.commonStations[0];


            int s1 = map.stations1.FindIndex((i) => currentStation.GUID == i.GUID);
            int s2 = map.stations2.FindIndex((i) => currentStation.GUID == i.GUID);

            List<Map.Station> nearStations = new List<Map.Station>();

            if (s1 != -1)
            {
                if (s1 + 1 < map.stations1.Count)
                    nearStations.Add(map.stations1[s1 + 1]);
                if (s1 - 1 >= 0)
                    nearStations.Add(map.stations1[s1 - 1]);
            }
            if (s2 != -1)
            {
                if (s2 + 1 < map.stations2.Count)
                    nearStations.Add(map.stations2[s2 + 1]);
                if (s2 - 1 >= 0)
                    nearStations.Add(map.stations2[s2 - 1]);
            }

            // List<Map.Station> distinct = nearStations.Distinct().ToList();

            foreach (Map.Station s in nearStations)
            {
                int i;
                if (map.stations1.Contains(s))
                {
                    i = map.stations1.IndexOf(s);
                    stationObjects1[i].GetComponent<Image>().color = Color.red;
                    Utils.EventBinder.Bind(stationObjects1[i].GetComponent<EventTrigger>(), EventTriggerType.PointerDown, (data) =>
                    {
                        DataManager.Singleton.PlayerData.nextStation = s.GUID;
                        DataManager.Singleton.PlayerData.currentPath = map.path1[i > map.stations1.IndexOf(currentStation) ? i : i - 1].GUID;
                        AsyncSceneLoader.Singleton.LoadScene("GenerateTest new UI");
                    });
                }
                else if (map.stations2.Contains(s))
                {
                    i = map.stations2.IndexOf(s);
                    stationObjects2[i].GetComponent<Image>().color = Color.red;
                    Utils.EventBinder.Bind(stationObjects2[i].GetComponent<EventTrigger>(), EventTriggerType.PointerDown, (data) =>
                    {
                        DataManager.Singleton.PlayerData.nextStation = s.GUID;
                        DataManager.Singleton.PlayerData.currentPath = map.path2[i > map.stations2.IndexOf(currentStation) ? i : i - 1].GUID;
                        AsyncSceneLoader.Singleton.LoadScene("GenerateTest new UI");
                    });
                }


            }
        }
    }
}
