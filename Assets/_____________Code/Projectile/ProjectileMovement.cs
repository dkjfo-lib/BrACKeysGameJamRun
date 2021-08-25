using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 100;
    public float drag = 10;

    Rigidbody2D Rigidbody { get; set; }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.drag = drag;
    }

    void FixedUpdate()
    {
        var input = (Vector2)transform.right;
        var velocity = input * Time.fixedDeltaTime * speed;
        Rigidbody.velocity += velocity;
    }
}
