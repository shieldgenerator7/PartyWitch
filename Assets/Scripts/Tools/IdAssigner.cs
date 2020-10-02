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
    [Tooltip("False: DoorTrigger.connectIds will be set to -1")]
    public bool autoReconnectDoors = true;

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

        //
        //Special DoorTrigger processing
        //
        List<DoorTrigger> doorTriggers = triggers.Where(t => t is DoorTrigger)
            .Cast<DoorTrigger>().ToList();
        //Set all connectedIds to -1
        doorTriggers.ForEach(d => d.connectedId = -1);
        //Attempt to reconnect doors
        if (autoReconnectDoors)
        {
            doorTriggers.ForEach(
                d =>
                {
                    //If it's still not connected to a door,
                    if (d.connectedId < 0)
                    {
                        //Search for suitable door
                        DoorTrigger d2 = doorTriggers.FirstOrDefault(
                            door => door.connectedId < 0
                            && door.connectScene == d.gameObject.scene.name
                            && d.connectScene == door.gameObject.scene.name
                            );
                        //If suitable door found,
                        if (d2)
                        {
                            //Connect the two doors
                            d.connectTo(d2);
                            d2.connectTo(d);
                        }
                    }
                }
                );
        }
    }
}
#endif
