using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectTrigger : EventTrigger
{
    [Tooltip("The title of the dialogue to play")]
    public string title;

    protected override void Start()
    {
        //If this item has already been collected,
        if (FindObjectOfType<DialogueManager>().progressManager.hasActivated(this))
        {
            //Destroy it
            destroy();
        }

        //Parent start up process
        base.Start();
    }

    protected override void triggerEvent()
    {
        //Find dialogue path by its title
        if (title != "" && title != null)
        {
            FindObjectOfType<DialogueManager>().playDialogue(title);
            destroy();
        }
    }

    private void destroy()
    {
        Destroy(this);
        Destroy(transform.parent.gameObject);
    }
}
