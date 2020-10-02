using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Triggers a dialogue cutscene
/// </summary>
public class DialogueTrigger : EventTrigger
{
    public string title;
    public List<string> characters;

    protected override void triggerEvent()
    {
        //Find dialogue path by its title
        if (title != "" && title != null)
        {
            FindObjectOfType<DialogueManager>().playDialogue(title);
        }
        //Find dialogue path by characters
        else
        {
            FindObjectOfType<DialogueManager>().playDialogue(characters);
        }
    }
}
