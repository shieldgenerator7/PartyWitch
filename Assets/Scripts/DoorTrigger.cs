using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Triggers going through a door to another area
/// </summary>
public class DoorTrigger : EventTrigger
{
    public class Door
    {
        public int id;
        /// <summary>
        /// The id of the door this door is connected to
        /// </summary>
        public int connectId;

        public Door(int id, int connectId)
        {
            this.id = id;
            this.connectId = connectId;
        }
    }

    public int id = -1;
    public int connectedId = -1;
    public string connectScene;

    public Door door;

    protected override void Start()
    {
        base.Start();
        door = new Door(id, connectedId);
        if (id < 0)
        {
            throw new ArgumentException(
                "DoorTrigger Id is invalid on object " + gameObject.name
                + " in scene " + gameObject.scene.name + ". "
                + "Id must be 0 or greater (Assign it an Id). "
                + "Invalid value: " + id
                );
        }
        if (connectedId < 0)
        {
            throw new ArgumentException(
                "DoorTrigger Connected Id is invalid on object " + gameObject.name
                + " in scene " + gameObject.scene.name + ". "
                + "Connected Id must be 0 or greater (Connect two DoorTriggers together). "
                + "Invalid value: " + connectedId
                );
        }
        if (this.connectScene == null || this.connectScene == "")
        {
            throw new ArgumentException(
                "DoorTrigger Connect Scene is invalid on object " + gameObject.name
                + " in scene " + gameObject.scene.name + ". "
                + "Connected Scene must be a valid scene name. "
                + "Invalid value: " + connectScene
                );
        }
        DoorTrigger dupIdDoor = FindObjectsOfType<DoorTrigger>()
            .FirstOrDefault(
                d => d.gameObject.scene == this.gameObject.scene
                && d.id == this.id
                && d != this
            );
        if (dupIdDoor)
        {
            throw new ArgumentException(
                "DoorTrigger has a duplicate Id! "
                + "GameObject " + gameObject.name + " and " + dupIdDoor.name
                + " in scene " + gameObject.scene.name
                + " have the same Id: " + this.id+". "
                + "Assign these doors unique Ids."
                );
        }
    }

    public override void triggerEvent()
    {
        FindObjectOfType<AreaManager>().jumpToDoor(connectScene, door);
    }

    public void connectTo(DoorTrigger door)
    {
        this.connectedId = door.id;
        this.connectScene = door.gameObject.scene.name;
    }
}
