using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemCollectTrigger))]
[CanEditMultipleObjects]
public class ItemCollectTriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Assign Id"))
        {
            assignIds();
        }
    }

    private void assignIds()
    {
        List<ItemCollectTrigger> items = targets.Cast<ItemCollectTrigger>().ToList();
        Undo.RecordObjects(items.ToArray(), "Assign Ids to ItemCollectTrigger objects.");
        //Unset ids to allow potential for first id to be 0
        items.ForEach(item => item.itemId = -1);
        //Find the highest number among available ids
        int maxKnownId = FindObjectsOfType<ItemCollectTrigger>().Max(item => item.itemId);
        //Assign unique ids
        items.ForEach(
            item =>
            {
                item.itemId = maxKnownId + 1;
                maxKnownId++;
            }
            );
    }
}