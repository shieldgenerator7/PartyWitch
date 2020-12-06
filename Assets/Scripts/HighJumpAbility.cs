using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJumpAbility : PlayerAbility
{
    public float newJumpSpeed = 10;

    private float originalJumpSpeed;
    protected override void init()
    {
        originalJumpSpeed = playerController.jumpSpeed;
        playerController.jumpSpeed = newJumpSpeed;
    }
    protected override void OnDisable()
    {
        playerController.jumpSpeed = originalJumpSpeed;
    }
}
