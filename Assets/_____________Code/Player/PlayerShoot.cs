using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public VectorValue sightInput;
    public Pipe_Value Pipe_ManaValue;
    [Space]
    public float maxMana = 12;
    public float curMana = 12;
    public float manaRechargePerSecond = 4;
    [Space]
    public Pipe_Weapon Pipe_Weapon;
    public Weapon currentWeapon => Pipe_Weapon.GetCurrentWeapon();
    [Space]
    public Pipe_SoundsPlay Pipe_SoundsPlay;
    public ClipsCollection sound_manaExhausted;
    public ClipsCollection sound_manaRecharged;

    bool manaWasExhausted = false;
    bool manaDoesntRecharge = false;

    float timeLastShoot = -100;

    int currentWeaponId => Pipe_Weapon.currentWeapon;

    private void Start()
    {
        Pipe_ManaValue.maxValue = maxMana;
        Pipe_ManaValue.currentValue = curMana;
    }

    void Update()
    {
        RechargeMana();
        Shoot();
        //SwitchWeapon();
    }

    void RechargeMana()
    {
        if (manaDoesntRecharge) return;

        curMana += manaRechargePerSecond * Time.deltaTime;
        if (curMana > maxMana)
        {
            curMana = maxMana;
        }
        Pipe_ManaValue.currentValue = curMana;
    }

    void Shoot()
    {
        RotateToMouse();
        if (sightInput.value != Vector3.zero)
        {
            ShootPrimary();
        }
    }

    void RotateToMouse()
    {
        //Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 directionToMouse = mouseWorldPosition - (Vector2)transform.position;
        //transform.right = directionToMouse;

        if (sightInput.value != Vector3.zero)
            transform.right = sightInput.value;
    }

    void ShootPrimary()
    {
        if (manaWasExhausted) return;

        float TimePassed() => Time.timeSinceLevelLoad - timeLastShoot;
        if (TimePassed() > 1 / currentWeapon.shotsPerSecond)
        {
            Instantiate(currentWeapon.projectile, transform.position + transform.right, transform.rotation);
            timeLastShoot = Time.timeSinceLevelLoad;

            CalculateMana();
        }
    }

    void CalculateMana()
    {
        curMana -= currentWeapon.manaCost;
        if (curMana < 0)
        {
            StartCoroutine(CantShoot());
            curMana = 0;
        }
        Pipe_ManaValue.currentValue = curMana;
        StartCoroutine(CantRecharge());
    }

    IEnumerator CantShoot()
    {
        manaWasExhausted = true;
        Pipe_SoundsPlay.AddClip(new PlayClipData(sound_manaExhausted, transform.position));

        yield return new WaitUntil(() => curMana >= 4);

        manaWasExhausted = false;
        Pipe_SoundsPlay.AddClip(new PlayClipData(sound_manaRecharged, transform.position));
    }

    IEnumerator CantRecharge()
    {
        manaDoesntRecharge = true;

        yield return new WaitForSeconds(.25f);

        manaDoesntRecharge = false;
    }

    //void SwitchWeapon()
    //{
    //    var mouseWheel = Input.GetAxis("Mouse ScrollWheel");
    //    if (mouseWheel > 0)
    //    {
    //        var uw = Pipe_Weapon.unlockedWeapons;
    //        currentWeaponId += 1;
    //        if (currentWeaponId > uw.Length - 1)
    //            currentWeaponId = 0;
    //        Pipe_Weapon.currentWeapon = currentWeaponId;
    //    }
    //    if (mouseWheel < 0)
    //    {
    //        var uw = Pipe_Weapon.unlockedWeapons;
    //        currentWeaponId -= 1;
    //        if (currentWeaponId < 0)
    //            currentWeaponId = uw.Length - 1;
    //        Pipe_Weapon.currentWeapon = currentWeaponId;
    //    }
    //}
}
