﻿
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class DialoguePath
{
    public string title = "Dialogue Title";
    public List<Quote> quotes = new List<Quote>();

    /// <summary>
    /// Restores temp variables after being read in
    /// </summary>
    public void inflate()
    {
        quotes.ForEach(
            q => q.path = this
            );
    }

    /// <summary>
    /// Returns a list of the characters in this dialogue path
    /// </summary>
    public List<string> Characters
        => quotes.Select(q => q.characterName).Distinct().ToList();

    /// <summary>
    /// Returns true if all the required characters are in this dialogue path.
    /// Allows for extra characters not mentioned
    /// </summary>
    /// <param name="requiredCharacters"></param>
    /// <returns></returns>
    public bool allCharactersPresent(List<string> requiredCharacters)
    {
        List<string> chars = Characters;
        foreach (string chr in requiredCharacters)
        {
            //If one required character is not in this dialogue path,
            if (!chars.Contains(chr))
            {
                //Then not all are present
                return false;
            }
        }
        //All characters present
        return true;
    }
}
