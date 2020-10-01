using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EventTrigger),true)]
[CanEditMultipleObjects()]
public class EventTriggerEditor : Editor
{
    

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        List<EventTrigger> triggers = targets.ToList()
            .Cast<EventTrigger>().ToList();
        //If they're all of the right type(s),
        if (triggers.TrueForAll(
            t => t is DialogueTrigger || t is ItemCollectTrigger
            ))
        {
            //Convert to DialogueTrigger
            if (GUILayout.Button("Convert to DialogueTrigger"))
            {
                List<ItemCollectTrigger> converts = triggers.Where(
                    t => !(t is DialogueTrigger)
                    ).Cast<ItemCollectTrigger>().ToList();
                Undo.RecordObjects(
                    converts.ToArray(),
                    "Convert "+ converts.Count+" to DialogueTriggers"
                    );
                converts.ForEach(
                    t =>
                    {
                        DialogueTrigger dialogue = t.gameObject.AddComponent<DialogueTrigger>();
                        dialogue.title = t.title;
                        DestroyImmediate(t);
                        EditorUtility.SetDirty(dialogue);
                    }
                    );
                Debug.Log("Converted " + converts.Count + " EventTriggers to DialogueTrigger");
            }
            //Convert to ItemCollectTrigger
            if (GUILayout.Button("Convert to ItemCollectTrigger"))
            {
                List<DialogueTrigger> converts = triggers.Where(
                    t => !(t is ItemCollectTrigger)
                    ).Cast<DialogueTrigger>().ToList();
                Undo.RecordObjects(
                    converts.ToArray(),
                    "Convert " + converts.Count + " to ItemCollectTriggers"
                    );
                converts.ForEach(
                    t =>
                    {
                        ItemCollectTrigger item = t.gameObject.AddComponent<ItemCollectTrigger>();
                        item.title = t.title;
                        DestroyImmediate(t);
                        EditorUtility.SetDirty(item);
                    }
                    );
                Debug.Log("Converted " + converts.Count + " EventTriggers to ItemCollectTriggers");
            }
        }
        else
        {
            GUILayout.Box("Can only convert between DialogueTrigger and ItemCollectTrigger.");
        }
    }
}
