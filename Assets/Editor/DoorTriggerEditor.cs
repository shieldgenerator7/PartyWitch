using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DoorTrigger))]
[CanEditMultipleObjects]
public class DoorTriggerEditor : EventTriggerEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //Connect button
        if (targets.Length == 2)
        {
            if (GUILayout.Button("Connect"))
            {
                DoorTrigger door1 = (DoorTrigger)targets[0];
                DoorTrigger door2 = (DoorTrigger)targets[1];
                Undo.RecordObjects(
                    targets,
                    "Connect " + door1.name + " and " + door2.name + " together."
                    );
                door1.connectTo(door2);
                door2.connectTo(door1);
            }
        }
        else
        {
            GUILayout.Box("Select two DoorTriggers for more options.");
        }
    }    
}
