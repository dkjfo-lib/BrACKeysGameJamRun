using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BulletColor : MonoBehaviour
{
    public Color color;

    void Start()
    {
        GetComponentInChildren<TrailRenderer>().colorGradient.colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(color, 0),
            //new GradientColorKey(color, 1),
        };
        GetComponentInChildren<Light2D>().color = color;
    }
}
