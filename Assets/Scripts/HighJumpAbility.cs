using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJumpAbility : PlayerAbility
{
    protected override void OnEnable()
    {
        Debug.Log("High Jump Enabled");
    }
    protected override void OnDisable()
    {
        Debug.Log("High Jump Disabled");
    }
}
