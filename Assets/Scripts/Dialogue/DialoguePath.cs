
using System;
using System.Collections.Generic;

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
}
