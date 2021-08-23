using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementInput", menuName = "Movement/PlayerMovementInput")]
public class PlayerMovement : MovementInput
{
    public override Vector2 ReadMovementInput()
    {
        return CreateMovementInput();
    }

    private static Vector2 CreateMovementInput()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
            input -= Vector2.right;
        if (Input.GetKey(KeyCode.D))
            input += Vector2.right;
        if (Input.GetKey(KeyCode.S))
            input -= Vector2.up;
        if (Input.GetKey(KeyCode.W))
            input += Vector2.up;
        return input;
    }
}
