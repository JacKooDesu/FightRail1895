using UnityEngine;
using UnityEditor;
using JacDev.Data;

[CustomEditor(typeof(MapData))]
public class MapDataEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
    }
}