using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJoystick : Joystick
{
    public VectorValue Output;

    private void Update()
    {
        Output.value = Direction.normalized;
    }
}