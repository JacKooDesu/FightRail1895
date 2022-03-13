using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JacDev.Item;

[CustomEditor(typeof(DropTable))]
public class DropTableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UpdateSettingName("modDropSettings","mod");
        UpdateSettingName("itemDropSettings","item");
    }

    void UpdateSettingName(string listName,string itemName)
    {
        var settings = serializedObject.FindProperty(listName);
        var arraySize = settings.arraySize;

        for (int i = 0; i < arraySize; ++i)
        {
            var item = settings.GetArrayElementAtIndex(i);
            var name = item.FindPropertyRelative("name");
            var setting = item.FindPropertyRelative(itemName).objectReferenceValue;   // 取得該掉落物

            name.stringValue = setting ? setting.name : "--none--";
        }
        serializedObject.ApplyModifiedProperties();
    }
}
