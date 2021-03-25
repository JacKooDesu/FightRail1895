using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    public Enemy enemy;

    public void SetEnemy(Enemy e)
    {
        enemy = e;
    }
}
