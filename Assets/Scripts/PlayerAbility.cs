using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : MonoBehaviour
{
    public string activationVariableName;

    protected PlayerController playerController;

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

    private void OnEnable()
    {
        playerController = GetComponent<PlayerController>();
        init();
    }
    protected abstract void init();
    protected abstract void OnDisable();
}
