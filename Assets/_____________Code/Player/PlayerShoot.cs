using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Pipe_Weapon Pipe_Weapon;
    public Weapon currentWeapon => Pipe_Weapon.GetCurrentWeapon();

    float timeLastShoot = -100;

    int currentWeaponId = 0;

    void Update()
    {
        Shoot();
        SwitchWeapon();
    }

    void Shoot()
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
        if (TimePassed() > 1 / currentWeapon.shotsPerSecond)
        {
            Instantiate(currentWeapon.projectile, transform.position + transform.right, transform.rotation);
            timeLastShoot = Time.timeSinceLevelLoad;
        }
    }

    void SwitchWeapon()
    {
        var mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel > 0)
        {
            var uw = Pipe_Weapon.unlockedWeapons;
            currentWeaponId += 1;
            if (currentWeaponId > uw.Length - 1)
                currentWeaponId = 0;
            Pipe_Weapon.currentWeapon = currentWeaponId;
        }
        if (mouseWheel < 0)
        {
            var uw = Pipe_Weapon.unlockedWeapons;
            currentWeaponId -= 1;
            if (currentWeaponId < 0)
                currentWeaponId = uw.Length - 1;
            Pipe_Weapon.currentWeapon = currentWeaponId;
        }
    }
}
