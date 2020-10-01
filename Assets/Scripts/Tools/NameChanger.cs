using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Tool used to mass-change a character's name in DialogueTriggers
/// </summary>
public class NameChanger : MonoBehaviour
{

    public string oldName;
    public string newName;

    public void changeName()
    {
        List<DialogueTrigger> dialogueTriggers = FindObjectsOfType<DialogueTrigger>()
            .Where(d => d.characters.Contains(oldName)).ToList();
        if (dialogueTriggers.Count == 0)
        {
            Debug.Log("NameChanger: Unable to find any DialogueTriggers with the name \"" + oldName+"\"");
            return;
        }
        Undo.RecordObjects(
            dialogueTriggers.ToArray(),
            "\"" + oldName + "\" -> \"" + newName
            + "\" (x" + dialogueTriggers.Count + ")"
            );
        dialogueTriggers.ForEach(
            d =>
            {
                for (int i = 0; i < d.characters.Count; i++)
                {
                    if (d.characters[i] == oldName)
                    {
                        d.characters[i] = newName;
                    }
                }
            }
            );
        Debug.Log("NameChanger: Changed \"" + oldName + "\" into \"" + newName
            + "\" in " + dialogueTriggers.Count + " DialogueTriggers.");
    }
}
