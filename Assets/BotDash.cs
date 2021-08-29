using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDash : MonoBehaviour, IBotMovement
{
    public float normalSpeed = 100;
    public float maintainSpeedValue = .97f;
    public float drag = 10;
    [Space]
    public Vector2 keepDistance = new Vector2(4, 6);
    public bool KeepsGoodDistance { get; private set; }
    public bool Moves { get; private set; }
    [Space]
    public float timeInDash = .5f;
    public float timeBetweenDashes = 1.2f;
    Vector2 input = Vector2.zero;

    Rigidbody2D Rigidbody { get; set; }
    BotSight BotSight { get; set; }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        BotSight = GetComponentInChildren<BotSight>();
        Rigidbody.drag = drag;
        StartCoroutine(ManageDash());
    }

    private IEnumerator ManageDash()
    {
        while (true)
        {
            do
            {
                yield return new WaitForSeconds(.2f);
                input = CreateMovementInput();
            } while (input == Vector2.zero);

            Moves = true;
            yield return new WaitForSeconds(timeInDash);

            input = Vector2.zero;
            Moves = false;
            yield return new WaitForSeconds(timeBetweenDashes - .2f);
        }
    }

    void FixedUpdate()
    {
        UpdateSight();
        if (input != Vector2.zero)
        {
            var velocity = input * Time.fixedDeltaTime * normalSpeed;
            Rigidbody.velocity = velocity;
        }
        else
        {
            Rigidbody.velocity *= maintainSpeedValue;
        }
    }

    void UpdateSight()
    {
        if (BotSight.canSeeTarget)
        {
            var distanceSqr = BotSight.targetVector.sqrMagnitude;
            if (distanceSqr < keepDistance.y * keepDistance.y &&
                distanceSqr > keepDistance.x * keepDistance.x)
            {
                KeepsGoodDistance = true;
            }
            else 
            {
                KeepsGoodDistance = false;
            }
        }
        else
        {
            KeepsGoodDistance = false;
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
            }
            else if (distanceSqr < keepDistance.x * keepDistance.x)
            {
                input = -BotSight.targetDirection;
            }
        }
        return input;
    }
}
