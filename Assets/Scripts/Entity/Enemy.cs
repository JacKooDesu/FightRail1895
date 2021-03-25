using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵方單位所有設定
[System.Serializable, CreateAssetMenu(fileName = "Enemy", menuName = "JacDev/Create Enemy", order = 1)]
public class Enemy : ScriptableObject
{
    public string enemyName = "None";

    public enum EnemyType   // 種族
    {
        Aboriginal,
        Japanese,
        Han
    }

    public EnemyType type;

    public MeshRenderer model;  // 模型
    public Sprite icon; // 圖示

    public float movementSpeed = 5.0f;  // 移動速度
    public float health = 20.0f;    // 血量
    public float damage = 5.0f;     // 傷害，之後可寫成獨立類別，與種族寫在一起
    public float attactSpeed = 1.0f; // 攻擊速度，每秒攻擊次數

    public List<Item> dropList = new List<Item>();  // 掉落表



}
