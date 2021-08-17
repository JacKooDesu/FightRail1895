using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    [System.Serializable, CreateAssetMenu(fileName = "Enemy List", menuName = "JacDev/Enemy/Enemy List", order = 1)]
    public class EnemyList : ScriptableObject
    {
        public List<Enemy> enemies = new List<Enemy>();
    }
}

