using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : MonoBehaviour
{
    public string activationVariableName;

    public void registerDelegates()
    {
        ProgressManager progress = FindObjectOfType<DialogueManager>().progressManager;
        progress.onVariableChange -= checkActivate;
        progress.onVariableChange += checkActivate;
    }

    private void checkActivate(string name, int oldValue, int newValue)
    {
        if (name == activationVariableName)
        {
            enabled = newValue > 0;
        }
    }

    protected abstract void OnEnable();
    protected abstract void OnDisable();
}
