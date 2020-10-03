using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Converts a dialogue variable into a string
/// </summary>
public class StringBank : MonoBehaviour
{
    [Serializable]
    public class StringBankEntry
    {
        public string variableName;
        public string str;
    }
    public List<StringBankEntry> bankEntries;

    public bool Contains(string varName)
        => bankEntries.Any(be => be.variableName == varName);

    public string getString(string varName)
        => bankEntries.FirstOrDefault(
            be => be.variableName == varName
            ).str;
}
