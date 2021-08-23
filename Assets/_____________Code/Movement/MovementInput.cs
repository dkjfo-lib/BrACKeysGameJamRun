using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementInput : ScriptableObject
{
    public abstract Vector2 ReadMovementInput();
}
