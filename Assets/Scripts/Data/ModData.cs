using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Mod;
using JacDev.Entity;

namespace JacDev.Data
{
    [System.Serializable]
    public class ModData : MonoBehaviour
    {
        public EntityEnums.EntityType entityBindType;
        public int bindingIndex;   // 0 代表沒有綁
        public int modSettingIndex;  // Mod Setting index
        public int rank;
    }
}
