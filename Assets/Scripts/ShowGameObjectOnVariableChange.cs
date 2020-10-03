using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameObjectOnVariableChange : MonoBehaviour
{
    [Tooltip("Action will be taken if this variable is at or above the value.")]
    public string requiredVariable;
    [Tooltip("Action will be taken if the variable is at or above this value.")]
    public int requiredValue;
    public enum ActionType
    {
        ENABLE,
        DISABLE
    }
    public ActionType actionToTake = ActionType.ENABLE;
    public ActionType elseActionToTake = ActionType.DISABLE;

    private void Start()
    {
        int curValue =FindObjectOfType<DialogueManager>()
            .progressManager.get(requiredVariable);
        checkTakeAction(requiredVariable, 0, curValue);
        FindObjectOfType<DialogueManager>()
            .progressManager.onVariableChange -= checkTakeAction;
        FindObjectOfType<DialogueManager>()
            .progressManager.onVariableChange += checkTakeAction;
    }

    private void OnDestroy()
    {
        FindObjectOfType<DialogueManager>()
            .progressManager.onVariableChange -= checkTakeAction;
    }

    /// <summary>
    /// When a scene loads, DialogueManager registers this function
    /// with the ProgressManager.OnVariableChange delegate
    /// </summary>
    public void checkTakeAction(string varName, int oldValue, int newValue)
    {
        if (varName == requiredVariable)
        {
            if (newValue >= requiredValue)
            {
                takeAction(actionToTake);
            }
            else
            {
                takeAction(elseActionToTake);
            }
        }
    }

    void takeAction(ActionType action)
    {
        switch (action)
        {
            case ActionType.ENABLE: gameObject.SetActive(true); break;
            case ActionType.DISABLE: gameObject.SetActive(false); break;
            default: throw new ArgumentException("Action type not supported: " + actionToTake);
        }
    }
}
