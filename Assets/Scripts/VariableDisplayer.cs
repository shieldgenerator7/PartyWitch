using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VariableDisplayer : VariableListener
{
    public const string VALUE_PLACEHOLDER = "[x]";

    public string variableName;
    public string displayString = VALUE_PLACEHOLDER;
    public float showDuration = 5;
    public TMP_Text uiText;

    private float showStartTime = -1;

    private void Start()
    {
        showDisplay(false);
        if (!displayString.Contains(VALUE_PLACEHOLDER))
        {
            Debug.LogError(
                "VariableDisplayer requires its display string to contain \""
                + VALUE_PLACEHOLDER
                + "\" or it will not show the value of the variable.",
                this
                );
        }
    }

    protected override void checkVariable(string varName, int oldValue, int newValue)
    {
        if (varName == variableName)
        {
            uiText.text = displayString.Replace(VALUE_PLACEHOLDER, "" + newValue);
            showDisplay();
        }
    }

    private void showDisplay(bool show = true)
    {
        gameObject.SetActive(show);
        showStartTime = (show)
            ? Time.time
            : -1;
    }

    private void Update()
    {
        if (showStartTime >= 0 && Time.time > showStartTime + showDuration)
        {
            showDisplay(false);
        }
    }
}
