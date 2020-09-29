using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DoorTrigger))]
[CanEditMultipleObjects]
public class DoorTriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Assign Id"))
        {
            assignIds();
        }
        if (targets.Length == 2)
        {
            if (GUILayout.Button("Connect"))
            {
                DoorTrigger door1 = (DoorTrigger)targets[0];
                DoorTrigger door2 = (DoorTrigger)targets[1];
                door1.connectTo(door2);
                door2.connectTo(door1);
            }
        }
        else
        {
            GUILayout.Box("Select two DoorTriggers for more options.");
        }
    }

    private void assignIds()
    {
        List<DoorTrigger> doors = new List<DoorTrigger>(
            Array.ConvertAll(targets, t => (DoorTrigger)t)
            );
        int maxKnownId = FindObjectsOfType<DoorTrigger>().Max(d => d.id);


    }
}
