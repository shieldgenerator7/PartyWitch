using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays dialogue
/// </summary>
public class DialoguePlayer : MonoBehaviour
{
    public float delayBetweenQuotes = 1;

    private int index = 0;
    private DialoguePath path;
    private float lastQuoteTime = -1;
    
    public void playDialogue(DialoguePath path)
    {
        index = 0;
        this.path = path;
        lastQuoteTime = Time.time;
    }

    public void stopDialogue()
    {
        lastQuoteTime = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastQuoteTime >= 0 
            && Time.time > lastQuoteTime + delayBetweenQuotes)
        {
            lastQuoteTime = Time.time;
            Debug.Log(
                path.quotes[index].characterName + ": "
                + path.quotes[index].text
                );
            index++;
            if (index >= path.quotes.Count)
            {
                stopDialogue();
            }
        }
    }
}
