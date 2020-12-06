using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpAbility : PlayerAbility
{
    public bool enableExtraJumps = false;
    private int _extraJumps;
    private int extraJumps
    {
        get => _extraJumps;
        set
        {
            _extraJumps = value;
            doubleJumpIndicator.color = (_extraJumps > 0)
                ? Color.white
                : noExtraJumpColor;
        }
    }
    public int extraJumpsValue;

    public SpriteRenderer doubleJumpIndicator;
    public Color noExtraJumpColor = new Color(1, 1, 1, 0.3f);

    protected override void init()
    {
        //playerController.onpl
        Debug.Log("Double Jump Enabled");
    }

    protected override void OnDisable()
    {
        Debug.Log("Double Jump Disabled");
    }
}
