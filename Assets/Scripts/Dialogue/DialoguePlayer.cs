using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Plays dialogue
/// </summary>
public class DialoguePlayer : MonoBehaviour
{
    public Canvas dialogueCanvas;
    public Image charPortrait;
    public TMP_Text charName;
    public TMP_Text charQuote;

    private int index = 0;
    private DialoguePath path;

    public delegate void DialogueDelegate(DialoguePath path);
    public DialogueDelegate onDialogueStarted;
    public DialogueDelegate onDialogueEnded;

    public bool Playing => path != null;

    public void playDialogue(DialoguePath path)
    {
        index = 0;
        this.path = path;
        onDialogueStarted?.Invoke(path);
        InteractUI.instance.suppress(this);
        if (path.quotes.Count > 0)
        {
            //UI
            dialogueCanvas.gameObject.SetActive(true);
            //Show the first quote
            advanceDialogue();
            //Subscribe to Interact button
            PlayerController.OnPlayerInteract += advanceDialogue;
            PlayerController.OnPlayerJump += advanceDialogue;
        }
        else
        {
            stopDialogue();
        }
    }

    public void stopDialogue()
    {
        //UI
        dialogueCanvas.gameObject.SetActive(false);
        onDialogueEnded?.Invoke(path);
        InteractUI.instance.suppress(this, false);
        //Unsubscribe from Interact button
        PlayerController.OnPlayerInteract -= advanceDialogue;
        PlayerController.OnPlayerJump -= advanceDialogue;
        //Unset path
        this.path = null;
    }

    // Update is called once per frame
    void advanceDialogue()
    {
        if (index >= path.quotes.Count)
        {
            stopDialogue();
            return;
        }
        displayQuote(path.quotes[index]);
        index++;
    }

    private void displayQuote(Quote quote)
    {
        charPortrait.sprite = Resources.Load<Sprite>("DialogueFaces/" + quote.imageName);
        charName.text = quote.characterName;
        charQuote.text = quote.text;
    }
}
