using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float normalSpeed = 100;
    public float maintainSpeedValue = .97f;
    public float drag = 10;

    Rigidbody2D Rigidbody { get; set; }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.drag = drag;
    }

    void FixedUpdate()
    {
        var input = CreateMovementInput();
        if (input != Vector2.zero)
        {
            var speed = Input.GetKey(KeyCode.LeftShift) ?
                normalSpeed / 3 :
                normalSpeed;
            var velocity = input * Time.fixedDeltaTime * speed;
            Rigidbody.velocity += velocity;
        }
        else
        {
            Rigidbody.velocity *= maintainSpeedValue;
        }
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
