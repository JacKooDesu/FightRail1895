// using UnityEngine;
// using UnityEditor;
// using JacDev.Item;
// using UnityEngine.UIElements;

// [CustomPropertyDrawer(typeof(DropTable.ModDropSetting))]
// public class LevelSettingDrawer : PropertyDrawer
// {
//     public override VisualElement CreatePropertyGUI(SerializedProperty property)
//     {
//         return base.CreatePropertyGUI(property);
//     }

//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//     {
//         // base.OnGUI(position, property, label);
//         EditorGUI.BeginProperty(position, label, property);

//         position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//         var indent = EditorGUI.indentLevel;
//         EditorGUI.indentLevel = 0;

//         int iter = 0;
//         UpdateQuality(property, "copper", position, iter);
//         iter++;
//         UpdateQuality(property, "silver", position, iter);


//         EditorGUI.EndProperty();
//     }

//     void UpdateQuality(SerializedProperty property, string name, Rect position, int iter)
//     {
//         var mod = property.FindPropertyRelative(name);
//         var sliderRect = new Rect(position.x, position.y + 35*iter, 200, position.height);

//         //EditorGUI.PropertyField(sliderRect, property.FindPropertyRelative("min"), GUIContent.none);
//         float min = mod.FindPropertyRelative("min").intValue;
//         float max = mod.FindPropertyRelative("max").intValue;
//         EditorGUI.MinMaxSlider(sliderRect, ref min, ref max, 0f, 100f);


//         var target = fieldInfo.GetValue(mod.serializedObject.targetObject) as DropTable.ModDropSetting;
//         UnityEngine.Debug.Log(mod.objectReferenceValue.name);
//         target.SetLevelMin(iter,(int)min);
//         target.SetLevelMax(iter,(int)max);
//     }

//     // void UpdateLevelSettingSlider()
//     // {
//     //     DropTable.ModDropSetting target = (DropTable.ModDropSetting)this.target;

//     //     var levels = target.GetLevelArray();
//     //     for (int j = 0; j < levels.Length; ++j)
//     //     {
//     //         float min = levels[j].min;
//     //         float max = levels[j].max;
//     //         EditorGUILayout.MinMaxSlider(ref min, ref max, 0f, 100f);
//     //         target.SetLevelMin(j, (int)min);
//     //         target.SetLevelMax(j, (int)max);
//     //     }
//     // }
// }