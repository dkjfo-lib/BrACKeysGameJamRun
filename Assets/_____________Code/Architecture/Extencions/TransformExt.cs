using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExt 
{
    public static Transform GetClosest(this Transform mtr, IEnumerable<Transform> transforms)
    {
        Transform closest = null;
        float minDistance = float.MaxValue;
        foreach (var other in transforms)
        {
            var sqrMagnitude = (other.position - mtr.position).sqrMagnitude;
            if (sqrMagnitude < minDistance)
            {
                minDistance = sqrMagnitude;
                closest = other;
            }
        }
        return closest;
    }
}
