using UnityEngine;
using System.Collections.Generic;

namespace JacDev.Mod
{
    [CreateAssetMenu(fileName = "ModList", menuName = "JacDev/Mod/Create Mod List", order = 0)]
    public class ModList : ScriptableObject
    {
        public List<ModFactory> modList = new List<ModFactory>();
    }
}
