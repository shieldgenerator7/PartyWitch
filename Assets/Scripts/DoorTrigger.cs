using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int id;
    public int connectedId;
    public string connectScene;

    public Door door;

    protected override void Start()
    {
        base.Start();
        door = new Door(id, connectedId);
    }

    protected override void triggerEvent()
    {
        throw new System.NotImplementedException();
    }

    public void connectTo(DoorTrigger door)
    {
        this.connectedId = door.id;
        this.connectScene = door.gameObject.scene.name;
    }
}
