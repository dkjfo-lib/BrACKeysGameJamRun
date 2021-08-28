using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHittable : MonoBehaviour, IHittable
{
    public Pipe_Value healthPipe;
    [Space]
    public Faction faction = Faction.PlayerTeam;
    public Faction Faction => faction;

    public float hp = 10;

    private void Start()
    {
        if (healthPipe != null)
        {
            healthPipe.maxValue = hp;
            healthPipe.currentValue = hp;
        }
    }

    public void GetHit(Hit hit)
    {
        hp -= hit.damage;
        if (healthPipe != null)
        {
            healthPipe.currentValue = hp;
        }
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
