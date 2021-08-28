using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour, IBotMovement
{
    public float normalSpeed = 100;
    public float maintainSpeedValue = .97f;
    public float drag = 10;
    [Space]
    public Vector2 keepDistance = new Vector2(4, 6);
    public bool KeepsGoodDistance { get; private set; }

    Rigidbody2D Rigidbody { get; set; }
    BotSight BotSight { get; set; }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        BotSight = GetComponentInChildren<BotSight>();
        Rigidbody.drag = drag;
    }

    void FixedUpdate()
    {
        var input = CreateMovementInput();
        if (input != Vector2.zero)
        {
            var velocity = input * Time.fixedDeltaTime * normalSpeed;
            Rigidbody.velocity += velocity;
        }
        else
        {
            Rigidbody.velocity *= maintainSpeedValue;
        }
    }

    Vector2 CreateMovementInput()
    {
        Vector2 input = Vector2.zero;
        if (BotSight.canSeeTarget)
        {
            var distanceSqr = BotSight.targetVector.sqrMagnitude;
            if (distanceSqr > keepDistance.y * keepDistance.y)
            {
                input = BotSight.targetDirection;
                KeepsGoodDistance = false;
            }
            else if (distanceSqr < keepDistance.x * keepDistance.x)
            {
                input = -BotSight.targetDirection;
                KeepsGoodDistance = false;
            }
            else
            {
                KeepsGoodDistance = true;
            }
        }
        else
        {
            KeepsGoodDistance = false;
        }
        return input;
    }
}
