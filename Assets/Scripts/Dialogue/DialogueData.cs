using System;
using System.Collections.Generic;
using System.Linq;

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

    public DialoguePath getDialoguePath(string title)
    {
        return dialogues.FirstOrDefault(d => d.title == title);
    }

    public DialoguePath getDialoguePath(List<string> characters)
    {
        return dialogues.FirstOrDefault(d => d.allCharactersPresent(characters));
    }
}
