using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class EventTrigger : MonoBehaviour
{
    [Tooltip(
           "The unique id for this individual trigger. " +
           "Used to determine whether this has ever been triggered."
           )]
    public int id = -1;
    public string IdString => gameObject.scene.name + "_" + id;

    private Collider2D coll2d;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        coll2d = GetComponents<Collider2D>().FirstOrDefault(c2d => c2d.isTrigger == true);
        if (!coll2d)
        {
            Debug.LogError("" + this.GetType().Name + " requires a Collider2D with isTrigger set to true. "
                + "This one on GameObject " + gameObject.name + " has none.");
        }
        //Id must be valid
        if (id < 0)
        {
            throw new ArgumentException(
                "EventTrigger Id is invalid on object " + gameObject.name
                + " in scene " + gameObject.scene.name + ". "
                + "Id must be 0 or greater (Assign it an Id). "
                + "Invalid value: " + id
                );
        }
        //Id must be unique
        EventTrigger dupIdTrigger = FindObjectsOfType<EventTrigger>()
            .FirstOrDefault(
                t => t.gameObject.scene == this.gameObject.scene
                && t.id == this.id
                && t != this
            );
        if (dupIdTrigger)
        {
            throw new ArgumentException(
                "EventTrigger has a duplicate Id! "
                + "GameObject " + gameObject.name + " and " + dupIdTrigger.name
                + " in scene " + gameObject.scene.name
                + " have the same Id: " + this.id + ". "
                + "Assign these EventTriggers unique Ids."
                );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InteractUI.instance.registerTrigger(this, true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InteractUI.instance.registerTrigger(this, false);
        }
    }

    private void OnDestroy()
    {
        InteractUI.instance.registerTrigger(this, false);
    }

    public void processTrigger()
    {
        FindObjectOfType<DialogueManager>().progressManager.markActivated(this);
        triggerEvent();
    }

    protected abstract void triggerEvent();
}
