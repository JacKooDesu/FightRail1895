using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Entity
{
    [System.Serializable, CreateAssetMenu(fileName = "Train", menuName = "JacDev/Create Train", order = 1)]
    public class Train : ScriptableObject
    {
        public string trainName = "None";

        public MeshRenderer model;  // 模型
        public Sprite icon; // 圖示

        public float movementSpeed = 5.0f;  // 移動速度
        public float health = 20.0f;    // 血量

    }

}
