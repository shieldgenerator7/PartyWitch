using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectTrigger : EventTrigger
{
    [Tooltip("The title of the dialogue to play")]
    public string title;

    public override void triggerEvent()
    {
        //Find dialogue path by its title
        if (title != "" && title != null)
        {
            FindObjectOfType<DialogueManager>().playDialogue(title);
            Destroy(this);
            Destroy(transform.parent.gameObject);
        }
    }
}
