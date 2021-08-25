using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHittable : MonoBehaviour, IHittable
{
    public Faction faction = Faction.PlayerTeam;
    public Faction Faction => faction;

    public float hp = 10;

    public void GetHit(Hit hit)
    {
        hp -= hit.damage;
        if (hp < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
