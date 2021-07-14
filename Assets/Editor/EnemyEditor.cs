using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JacDev.Entity;
[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var enemy = target as Enemy;
        if (enemy.attackType != AttackType.Melee)
            enemy.projectile = EditorGUILayout.ObjectField(enemy.projectile, typeof(Projectile), true) as Projectile;

    }
}
