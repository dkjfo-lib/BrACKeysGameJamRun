using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Pipe_Weapon", menuName = "Pipes/Weapon")]
public class Pipe_Weapon : ScriptableObject
{
    public int currentWeapon = 0;
    [Space]
    public Weapon[] gameWeapons;
    public Weapon[] unlockedWeapons => gameWeapons.Where(s => s.unlocked).ToArray();
    public int weaponsUnlocked => unlockedWeapons.Length;

    public Weapon GetCurrentWeapon() => gameWeapons[currentWeapon];
}

[System.Serializable]
public struct Weapon
{
    public string name;
    [Space]
    public bool unlocked;
    public float shotsPerSecond;
    public float manaCost;
    public ProjectileHit projectile;
}
