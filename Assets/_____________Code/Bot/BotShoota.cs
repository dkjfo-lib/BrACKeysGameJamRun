using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShoota : MonoBehaviour
{
    public int ProjectileId = 0;
    public ProjectileHit[] Projectile;
    [Space]
    public float shootsPerSecond = 4;
    float timeLastShoot = -100;

    BotSight BotSight { get; set; }
    IBotMovement BotMovement { get; set; }

    private void Start()
    {
        BotSight = transform.parent.GetComponentInChildren<BotSight>();
        BotMovement = transform.GetComponentInParent<IBotMovement>();
    }

    void Update()
    {
        if (BotMovement.KeepsGoodDistance)
        {
            RotateToTarget();
            ShootPrimary();
        }
    }

    void RotateToTarget()
    {
        transform.right = BotSight.targetDirection;
    }

    void ShootPrimary()
    {
        float TimePassed() => Time.timeSinceLevelLoad - timeLastShoot;
        if (TimePassed() > 1 / shootsPerSecond)
        {
            Instantiate(Projectile[ProjectileId], transform.position + transform.right, transform.rotation);
            timeLastShoot = Time.timeSinceLevelLoad;
        }
    }
}
