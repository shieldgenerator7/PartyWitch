﻿using System;

[Serializable]
public class Quote
{

    public string characterName = "";
    public string text = "";
    /// <summary>
    /// The image filename without the folder path and without the file extension
    /// </summary>
    public string imageName = "";
    public string imageFileName = "";

    [NonSerialized]
    public DialoguePath path;

    public int Index => path.quotes.IndexOf(this);

    public Quote(string charName = "", string txt = "", string imageFileName = "")
    {
        this.characterName = charName;
        this.text = txt;
        this.imageFileName = imageFileName;
        //set image name
        string[] split = imageFileName.Split(new char[] { '\\', '/' });
        string name = split[split.Length - 1];
        int lastDotIndex = name.LastIndexOf('.');
        this.imageName = name.Substring(0, lastDotIndex);
    }
}
