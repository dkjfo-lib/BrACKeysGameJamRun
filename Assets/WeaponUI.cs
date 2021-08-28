using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Pipe_Weapon WeaponPipe;
    [Space]
    public Image[] weaponSlots;
    public Sprite[] weaponSprites;
    [Space]
    public Image currentWeaponBorder;

    int weaponsUnlocked;
    int currentWeapon;
    Vector3 currentWeaponBorderTargetPos;

    private void Start()
    {
        StartCoroutine(UpdateUnlock());
        StartCoroutine(UpdateCurrentWeapon());
    }

    IEnumerator UpdateUnlock()
    {
        while (true)
        {
            weaponsUnlocked = WeaponPipe.weaponsUnlocked;
            for (int i = 0; i < 3; i++)
            {
                if (i <= weaponsUnlocked)
                {
                    weaponSlots[i].color = Color.white;
                }
                else
                {
                    weaponSlots[i].color = new Color(1, 1, 1, 0);
                }
            }
            yield return new WaitUntil(() => weaponsUnlocked != WeaponPipe.weaponsUnlocked);
        }
    }

    IEnumerator UpdateCurrentWeapon()
    {
        var cellSize = 110;
        while (true)
        {
            currentWeapon = WeaponPipe.currentWeapon;
            currentWeaponBorderTargetPos = (currentWeapon - 1) * Vector3.right * cellSize + Vector3.up * 50;

            yield return new WaitUntil(() => currentWeapon != WeaponPipe.currentWeapon);
        }
    }

    private void Update()
    {
        currentWeaponBorder.rectTransform.localPosition = Vector3.Lerp(currentWeaponBorder.rectTransform.localPosition, currentWeaponBorderTargetPos, .6f);
    }
}
