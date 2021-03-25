using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵方單位所有設定
[System.Serializable, CreateAssetMenu(fileName = "Enemy", menuName = "JacDev/Create Enemy", order = 1)]
public class Enemy : ScriptableObject
{
    public string enemyName = "None";
    public float health = 20.0f;    // 血量
    public enum EnemyType   // 種族
    {
        Aboriginal,
        Japanese,
        Han
    }

    public EnemyType type;

    public MeshRenderer model;  // 模型
    public Sprite icon; // 圖示
    
}
