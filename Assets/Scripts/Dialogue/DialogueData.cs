using System;
using System.Collections.Generic;

[Serializable]
public class DialogueData
{
    public List<DialoguePath> dialogues;

	public DialogueData(List<DialoguePath> dialogues = null)
    {
        this.dialogues = dialogues;
        if (this.dialogues == null)
        {
            this.dialogues = new List<DialoguePath>();
        }
    }
}
