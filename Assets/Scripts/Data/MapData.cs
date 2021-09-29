using System;
using UnityEngine;
using JacDev.Map;
using System.Collections.Generic;

namespace JacDev.Data
{
    [Serializable]
    public class MapData : MonoBehaviour
    {
        public List<object> path1 = new List<object>();
        public List<object> path2 = new List<object>();
        public List<Station> commonStations = new List<Station>();
    }
}