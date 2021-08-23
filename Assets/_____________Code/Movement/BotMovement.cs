using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BotMovementInput", menuName = "Movement/BotMovementInput")]
public class BotMovement : MovementInput
{
    public override Vector2 ReadMovementInput()
    {
        return CreateMovementInput();
    }

    private static Vector2 CreateMovementInput()
    {
        Vector2 input = Vector2.zero;
        return input;
    }
}
