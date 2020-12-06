using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueManager))]
public class DialogueManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUI.enabled = EditorApplication.isPlaying;

        if (GUILayout.Button("Print Variables (Play Mode)"))
        {
            ((DialogueManager)target).progressManager.printVariables();
        }
    }
}
