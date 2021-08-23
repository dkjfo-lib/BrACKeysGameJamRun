using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    Faction Faction { get; }

    Vector2 GetHit(Hit hit);
}

public class Hit
{
    public int damage;
}

public enum Faction
{
    AlwaysHit,
    NeverHit,
    PlayerTeam,
    OpponentTeam,
}