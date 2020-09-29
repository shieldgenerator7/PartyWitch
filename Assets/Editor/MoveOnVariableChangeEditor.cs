using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

[CustomEditor(typeof(MoveOnVariableChange))]
public class MoveOnVariableChangeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Add position"))
        {
            MoveOnVariableChange movc = ((MoveOnVariableChange)target);
            movc.positions.Add(movc.transform.position);
        }
        DrawDefaultInspector();
    }
}
