using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHittable : MonoBehaviour, IHittable
{
    public Pipe_Value Addon_healthPipe;
    public ShakeOnDestroy Addon_ShakeOnHit;
    [Space]
    public Faction faction = Faction.PlayerTeam;
    public Faction Faction => faction;

    public float hp = 10;

    private void Start()
    {
        if (Addon_healthPipe != null)
        {
            Addon_healthPipe.maxValue = hp;
            Addon_healthPipe.currentValue = hp;
        }
    }

    public void GetHit(Hit hit)
    {
        hp -= hit.damage;
        if (Addon_ShakeOnHit != null) Addon_ShakeOnHit.DoShake();
        if (Addon_healthPipe != null) Addon_healthPipe.currentValue = hp;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
