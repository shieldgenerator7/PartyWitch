using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Subclasses of this script respond to variables being changed in the ProgressManager
/// </summary>
public abstract class VariableListener : MonoBehaviour
{
    void OnEnable()
    {
        FindObjectOfType<DialogueManager>().progressManager
            .onVariableChange -= checkVariable;
        FindObjectOfType<DialogueManager>().progressManager
            .onVariableChange += checkVariable;
    }

    //private void OnDisable()
    //{
    //    Debug.Log("var list on disable called");
    //    FindObjectOfType<DialogueManager>().progressManager
    //        .onVariableChange -= checkVariable;
    //}

    private void OnDestroy()
    {
        FindObjectOfType<DialogueManager>().progressManager
            .onVariableChange -= checkVariable;
    }

    protected abstract void checkVariable(string varName, int oldValue, int newValue);

}
