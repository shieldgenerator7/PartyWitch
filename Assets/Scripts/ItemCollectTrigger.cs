using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectTrigger : EventTrigger
{
    [Tooltip("The title of the dialogue to play")]
    public string title;

    protected override void triggerEvent()
    {
        //Find dialogue path by its title
        if (title != "" && title != null)
        {
            FindObjectOfType<DialogueManager>().playDialogue(title);
            Destroy(transform.parent.gameObject);
        }
    }
}
