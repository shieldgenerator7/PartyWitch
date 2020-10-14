using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplayContainer : VariableListener
{
    public Sprite Sprite
    {
        get => imgItem.sprite;
        set => imgItem.sprite = value;
    }
    public Image imgItem;
    public TMP_Text txtCount;

    protected override void checkVariable(string varName, int oldValue, int newValue)
    {
        updateText(newValue);
    }

    public void updateText(int value)
    {
        txtCount.text = (value > 1)
            ? "" + value
            : "";
    }
}
