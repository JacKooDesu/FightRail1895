using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JacDev.Entity;

namespace JacDev.Testing
{
    public class EnemyStatusWindow : EditorWindow
    {
        EnemyObject currentSelect;

        [MenuItem("Testing/敵方單位監視")]
        public static void ShowWindow()
        {
            GetWindow<EnemyStatusWindow>("敵方單位監視");
        }

        private void OnGUI()
        {
            if (Selection.gameObjects.Length == 0)
                return;

            SerializeEnemy();
        }

        void SerializeEnemy()
        {
            if (!Selection.gameObjects[Selection.gameObjects.Length - 1].GetComponent<EnemyObject>())
                return;

            currentSelect = Selection.gameObjects[Selection.gameObjects.Length - 1].GetComponent<EnemyObject>();
            var enemySetting = currentSelect.entitySetting as Enemy;

            EditorGUI.indentLevel++;
            GUILayout.Label("當前選取單位", EditorStyles.boldLabel);
            var objectField = EditorGUILayout.ObjectField(currentSelect, typeof(EnemyObject), true);

            GUILayout.Label("掉落表", EditorStyles.boldLabel);

            foreach (var str in enemySetting.dropTable.AllDropRateStatus(currentSelect))
            {
                EditorGUILayout.LabelField(str);
            }
        }
    }

}
