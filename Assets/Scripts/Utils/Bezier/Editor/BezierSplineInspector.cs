using UnityEngine;
using UnityEditor;

namespace JacDev.Utils
{
    [CustomEditor(typeof(BezierSpline))]
    public class BezierSplineInspector : Editor
    {
        BezierSpline spline;
        Transform handleTransform;
        Quaternion handleRotation;

        // 曲線平滑度
        const int stepsPerCurve = 10;
        const float directionScale = .5f;
        const float handleSize = .04f;
        const float pickSize = .06f;

        int selectIndex = -1;

        static Color[] modeColors = {
            Color.white,
            Color.magenta,
            Color.blue
        };

        private void OnSceneGUI()
        {
            spline = target as BezierSpline;
            handleTransform = spline.transform;
            handleRotation = Tools.pivotRotation == PivotRotation.Local ?
                handleTransform.rotation : Quaternion.identity;

            // 顯示三個點
            Vector3 p0 = ShowPoint(0);

            for (int i = 1; i < spline.ControlPointCount; i += 3)
            {
                Vector3 p1 = ShowPoint(i);
                Vector3 p2 = ShowPoint(i + 1);
                Vector3 p3 = ShowPoint(i + 2);

                // 渲染直線
                Handles.color = Color.gray;
                Handles.DrawLine(p0, p1);
                Handles.DrawLine(p2, p3);

                Handles.DrawBezier(p0, p3, p1, p2, Color.cyan, null, 2f);

                p0 = p3;
            }

            ShowDirections();
        }

        public override void OnInspectorGUI()
        {
            spline = target as BezierSpline;
            EditorGUI.BeginChangeCheck();

            // 設定是否環形
            bool loop = EditorGUILayout.Toggle("Loop", spline.Loop);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Toggle Loop");
                EditorUtility.SetDirty(spline);
                spline.Loop = loop;
            }

            // 僅顯示當前選擇的點
            if (selectIndex >= 0 && selectIndex < spline.ControlPointCount)
            {
                DrawSelectedPointInspector();
            }

            // Add Curve 按鈕
            if (GUILayout.Button("Add Curve"))
            {
                Undo.RecordObject(spline, "Add Curve");
                spline.AddCurve();
                EditorUtility.SetDirty(spline);
            }
        }

        // 顯示點
        Vector3 ShowPoint(int index)
        {
            Vector3 point = handleTransform.TransformPoint(spline.GetControlPoint(index));

            // 渲染所有操作把
            float size = HandleUtility.GetHandleSize(point);
            if (index == 0)
            {
                size *= 2f;
            }
            
            Handles.color = modeColors[(int)spline.GetControlPointMode(index)];
            if (Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.SphereHandleCap))
            {
                selectIndex = index;
                Repaint();
            }

            // 正在操作的曲線顯示移動工具
            if (selectIndex == index)
            {
                EditorGUI.BeginChangeCheck();
                point = Handles.DoPositionHandle(point, handleRotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(spline, "Move Point");
                    EditorUtility.SetDirty(spline);
                    spline.SetControlPoint(index, handleTransform.InverseTransformPoint(point));
                }
            }

            return point;
        }

        void ShowDirections()
        {
            Handles.color = Color.yellow;
            Vector3 point = spline.GetPoint(0f);

            Handles.DrawLine(point, point + spline.GetDirection(0f) * directionScale);

            int steps = stepsPerCurve * spline.CurveCount;
            for (int i = 1; i <= steps; ++i)
            {
                point = spline.GetPoint(i / (float)steps);
                Handles.DrawLine(
                    point,
                    point + spline.GetDirection(i / (float)steps) * directionScale);
            }
        }

        void DrawSelectedPointInspector()
        {
            GUILayout.Label("Selected Point");
            EditorGUI.BeginChangeCheck();

            Vector3 point = EditorGUILayout.Vector3Field("Position", spline.GetControlPoint(selectIndex));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Move Point");
                EditorUtility.SetDirty(spline);
                spline.SetControlPoint(selectIndex, point);
            }

            EditorGUI.BeginChangeCheck();
            BezierControlPointMode mode =
                (BezierControlPointMode)EditorGUILayout.EnumPopup("Mode", spline.GetControlPointMode(selectIndex));

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Change Point Mode");
                spline.SetControlPointMode(selectIndex, mode);
                EditorUtility.SetDirty(spline);
            }
        }
    }
}