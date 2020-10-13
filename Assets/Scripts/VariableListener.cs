using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Subclasses of this script respond to variables being changed in the ProgressManager
/// </summary>
public abstract class VariableListener : MonoBehaviour
{
    public string targetVariable = "variableName";

    void OnEnable()
    {
        FindObjectOfType<DialogueManager>().progressManager
            .onVariableChange -= processVariableChange;
        FindObjectOfType<DialogueManager>().progressManager
            .onVariableChange += processVariableChange;
    }

    //private void OnDisable()
    //{
    //    Debug.Log("var list on disable called");
    //    FindObjectOfType<DialogueManager>().progressManager
    //        .onVariableChange -= processVariableChange;
    //}

    private void OnDestroy()
    {
        FindObjectOfType<DialogueManager>().progressManager
            .onVariableChange -= processVariableChange;
    }

    private void processVariableChange(string varName, int oldValue, int newValue)
    {
        if (isTargetVariable(varName))
        {
            checkVariable(varName, oldValue, newValue);
        }
    }

    protected virtual bool isTargetVariable(string varName)
        => varName == targetVariable;
    protected abstract void checkVariable(string varName, int oldValue, int newValue);

}
