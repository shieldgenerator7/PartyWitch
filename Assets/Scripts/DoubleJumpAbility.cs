using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpAbility : PlayerAbility
{
    [Header("Settings")]
    public int extraJumpsValue;
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

    [Header("Components")]
    public SpriteRenderer doubleJumpIndicator;
    public Color noExtraJumpColor = new Color(1, 1, 1, 0.3f);

    [Header("Sound Effects")]
    public AudioClip doubleJumpSound;

    protected override void init()
    {
        extraJumps = extraJumpsValue;
        PlayerController.OnPlayerJump += DoubleJump;
        playerController.onGrounded += resetExtraJumps;
    }

    protected override void OnDisable()
    {
        PlayerController.OnPlayerJump -= DoubleJump;
        playerController.onGrounded -= resetExtraJumps;
    }

    private void DoubleJump()
    {
        if (!playerController.isGrounded && extraJumps > 0)
        {
            extraJumps--;
            playerController.Jump(doubleJumpSound);
        }
    }


    public void resetExtraJumps()
    {
        resetExtraJumps(0);
    }
    public void resetExtraJumps(int extraextras)
    {
        extraJumps = extraJumpsValue + extraextras;
    }
}
