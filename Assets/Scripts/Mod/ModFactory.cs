using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JacDev.Mod
{
    [System.Serializable, CreateAssetMenu(fileName = "new Mod", menuName = "JacDev/Create Mod", order = 1)]
    public class ModFactory : ScriptableObject
    {
        public string modName;
        public List<ModSetting> settings = new List<ModSetting>();
        public ModQualityFactory quality;
    }
}

