using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Converts a dialogue variable into a Sprite
/// </summary>
public class SpriteBank : MonoBehaviour
{
    [Serializable]
    public class SpriteBankEntry
    {
        public string variableName;
        public Sprite sprite;
    }
    public List<SpriteBankEntry> bankEntries;

    public bool Contains(string varName)
        => bankEntries.Any(be => be.variableName == varName);

    public Sprite getSprite(string varName)
        => bankEntries.FirstOrDefault(
            be => be.variableName == varName
            ).sprite;
}
