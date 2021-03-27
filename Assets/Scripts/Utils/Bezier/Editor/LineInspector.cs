using UnityEngine;
using UnityEditor;

namespace JacDev.Utils
{
    [CustomEditor(typeof(Line))]
    public class LineInspector : Editor
    {
        private void OnSceneGUI()
        {
            Line line = target as Line;

            // 重新定位
            Transform handleTransform = line.transform;
            Vector3 p0 = handleTransform.TransformPoint(line.p0);
            Vector3 p1 = handleTransform.TransformPoint(line.p1);

            // 定義轉向
            Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ?
                handleTransform.rotation : Quaternion.identity;

            // 渲染出線條
            Handles.color = Color.cyan;
            Handles.DrawLine(p0, p1);

            // 給予兩點移動模式，並將兩點變動寫入 Line 腳本
            EditorGUI.BeginChangeCheck();
            p0 = Handles.DoPositionHandle(p0, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(line, "Move Point");
                EditorUtility.SetDirty(line);
                line.p0 = handleTransform.InverseTransformPoint(p0);
            }
            EditorGUI.BeginChangeCheck();
            p1 = Handles.DoPositionHandle(p1, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(line, "Move Point");
                EditorUtility.SetDirty(line);
                line.p1 = handleTransform.InverseTransformPoint(p1);
            }


        }
    }
}