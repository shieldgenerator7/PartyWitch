using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class IdAssigner : MonoBehaviour
{
    [Tooltip("True: Set the Ids of EventTriggers that already have one.")]
    public bool overwriteExistingIds = true;

    public void assignIds()
    {
        //Get list of EventTriggers to assign ids to
        List<EventTrigger> allTriggers = Resources.FindObjectsOfTypeAll<EventTrigger>().ToList();
        List<EventTrigger> triggers = (overwriteExistingIds)
        ? allTriggers
        : allTriggers.Where(t => t.id < 0).ToList();

        //Record undo
        Undo.RecordObjects(
            triggers.ToArray(),
            "Assign Ids to " + triggers.Count + " EventTrigger objects."
            );
        //Unset ids to allow potential for first id to be 0
        triggers.ForEach(t => t.id = -1);
        //Find the highest number among available ids
        int maxKnownId = allTriggers.Max(t => t.id);
        //Assign unique ids
        triggers.ForEach(
            t =>
            {
                t.id = maxKnownId + 1;
                maxKnownId++;
            }
            );
    }
}
#endif
