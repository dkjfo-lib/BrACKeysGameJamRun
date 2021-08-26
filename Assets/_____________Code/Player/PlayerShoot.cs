using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public ProjectileHit Projectile;
    [Space]
    public float shootsPerSecond = 4;
    float timeLastShoot = -100;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RotateToMouse();
            ShootPrimary();
        }
    }

    void RotateToMouse()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = mouseWorldPosition - (Vector2)transform.position;
        transform.right = directionToMouse;
    }

    void ShootPrimary()
    {
        float TimePassed() => Time.timeSinceLevelLoad - timeLastShoot;
        if (TimePassed() > 1 / shootsPerSecond)
        {
            Instantiate(Projectile, transform.position + transform.right, transform.rotation);
            timeLastShoot = Time.timeSinceLevelLoad;
        }
    }
}
