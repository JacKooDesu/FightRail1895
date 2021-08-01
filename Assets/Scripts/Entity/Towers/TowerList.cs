using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    [System.Serializable, CreateAssetMenu(fileName = "Tower List", menuName = "JacDev/Tower/Tower List", order = 1)]
    public class TowerList : ScriptableObject
    {
        public List<Tower> towers = new List<Tower>();
        // 是否在player Data 新增塔解鎖List?
    }
}
