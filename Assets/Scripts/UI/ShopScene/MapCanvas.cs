using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        List<RectTransform> stationObjects1 = new List<RectTransform>();
        List<RectTransform> stationObjects2 = new List<RectTransform>();

        private void Start()
        {
            InitMap();
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
    }
}
