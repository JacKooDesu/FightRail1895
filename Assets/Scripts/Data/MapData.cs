using System;
using UnityEngine;
using JacDev.Map;
using System.Collections.Generic;

namespace JacDev.Data
{
    [System.Serializable]
    public class MapData
    {
        [Header("路線一")]
        public List<Station> stations1 = new List<Station>();
        public List<Path> path1 = new List<Path>();

        [Header("路線二")]
        public List<Station> stations2 = new List<Station>();
        public List<Path> path2 = new List<Path>();

        [Header("大站 (共點)")]
        public List<Station> commonStations = new List<Station>();
    }
}