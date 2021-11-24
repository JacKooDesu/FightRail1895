using UnityEngine;

namespace JacDev.Map
{
    [CreateAssetMenu(fileName = "Station Setting", menuName = "JacDev/Map/Create Station Setting", order = 0)]
    public class StationSetting : ScriptableObject
    {
        public Station station;
    }
}