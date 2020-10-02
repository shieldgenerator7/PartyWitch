using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IdAssigner))]
public class IdAssignerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Assign Ids"))
        {
            ((IdAssigner)target).assignIds();
        }
    }
}
