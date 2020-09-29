using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager
{
    private readonly Dictionary<string, int> data = new Dictionary<string, int>();

    public void set(string varName, int value = 0)
    {
        verify(varName);
        int oldValue = data[varName];
        data[varName] = value;
        onVariableChange?.Invoke(varName, oldValue, data[varName]);
    }

    public int get(string varName)
    {
        verify(varName);
        return data[varName];
    }

    public void add(string varName, int value = 1)
    {
        verify(varName);
        int oldValue = data[varName];
        data[varName] += value;
        onVariableChange?.Invoke(varName, oldValue, data[varName]);
    }

    public void multiply(string varName, int value = 2)
    {
        verify(varName);
        int oldValue = data[varName];
        data[varName] *= value;
        onVariableChange?.Invoke(varName, oldValue, data[varName]);
    }

    /// <summary>
    /// Makes sure the given variable is in the list
    /// If not, it initializes it to 0
    /// </summary>
    /// <param name="varName"></param>
    private void verify(string varName)
    {
        if (!data.ContainsKey(varName))
        {
            data[varName] = 0;
        }
    }

    public delegate void OnVariableChange(string varName, int oldValue, int newValue);
    public OnVariableChange onVariableChange;
}
