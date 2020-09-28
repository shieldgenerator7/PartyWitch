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
    public float delayBetweenQuotes = 1;

    public Canvas dialogueCanvas;
    public Image charPortrait;
    public TMP_Text charName;
    public TMP_Text charQuote;

    private int index = 0;
    private DialoguePath path;
    private float lastQuoteTime = -1;

    public delegate void DialogueDelegate(DialoguePath path);
    public DialogueDelegate onDialogueStarted;
    public DialogueDelegate onDialogueEnded;

    public void playDialogue(DialoguePath path)
    {
        index = 0;
        this.path = path;
        lastQuoteTime = Time.time;
        if (path.quotes.Count > 0)
        {
            //UI
            dialogueCanvas.gameObject.SetActive(true);
            //Show the first quote
            displayQuote(path.quotes[0]);
        }
        onDialogueStarted?.Invoke(path);
    }

    public void stopDialogue()
    {
        lastQuoteTime = -1;
        //UI
        dialogueCanvas.gameObject.SetActive(false);
        onDialogueEnded?.Invoke(path);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastQuoteTime >= 0
            && Time.time > lastQuoteTime + delayBetweenQuotes)
        {
            lastQuoteTime = Time.time;
            if (index >= path.quotes.Count)
            {
                stopDialogue();
                return;
            }
            displayQuote(path.quotes[index]);
            index++;
        }
    }

    private void displayQuote(Quote quote)
    {
        charPortrait.sprite = Resources.Load<Sprite>("DialogueFaces/" + quote.imageName);
        charName.text = quote.characterName;
        charQuote.text = quote.text;
    }
}
