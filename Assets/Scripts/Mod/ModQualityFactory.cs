using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Mod
{
    [System.Serializable, CreateAssetMenu(fileName = "Mod Quality Setting", menuName = "JacDev/Mod/new Quality Setting", order = 1)]
    public class ModQualityFactory : ScriptableObject
    {
        [System.Serializable]
        public class Setting
        {
            public float worstValue;
            public float bestValue;
        }

        public Setting Damage;
        public Setting AtkSpeed;
        public Setting Accurate;
        public Setting Poison;
        public Setting Burn;
        public Setting Dizzy;
        public Setting Capacity;
    }
}