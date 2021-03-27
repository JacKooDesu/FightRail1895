using UnityEngine;
using UnityEditor;

namespace JacDev.Utils
{
    [CustomEditor(typeof(BezierCurve))]
    public class BezierCurveInspector : Editor
    {
        BezierCurve curve;
        Transform handleTransform;
        Quaternion handleRotation;

        // 曲線平滑度
        const int lineSteps = 10;

        private void OnSceneGUI()
        {
            curve = target as BezierCurve;
            handleTransform = curve.transform;
            handleRotation = Tools.pivotRotation == PivotRotation.Local ?
                handleTransform.rotation : Quaternion.identity;

            // 顯示三個點
            Vector3 p0 = ShowPoint(0);
            Vector3 p1 = ShowPoint(1);
            Vector3 p2 = ShowPoint(2);

            // 渲染直線
            Handles.color = Color.gray;
            Handles.DrawLine(p0, p1);
            Handles.DrawLine(p1, p2);

            // 渲染曲線
            Handles.color = Color.cyan;
            Vector3 lineStart = curve.GetPoint(0f);
            for (int i = 1; i <= lineSteps; ++i)
            {
                Vector3 lineEnd = curve.GetPoint(i / (float)lineSteps);
                Handles.DrawLine(lineStart, lineEnd);
                lineStart = lineEnd;
            }

        }

        // 顯示點
        Vector3 ShowPoint(int index)
        {
            Vector3 point = handleTransform.TransformPoint(curve.points[index]);
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(curve, "Move Point");
                EditorUtility.SetDirty(curve);
                curve.points[index] = handleTransform.InverseTransformPoint(point);
            }

            return point;
        }
    }
}

