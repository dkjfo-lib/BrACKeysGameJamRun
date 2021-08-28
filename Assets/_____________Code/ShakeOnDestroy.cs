using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnDestroy : MonoBehaviour
{
    public Pipe_CamShakes Pipe_CamShakes;
    [Space]
    public bool shakeOnStart;
    public bool shakeOnDestroy;
    public ShakeAtributes ShakeAtributes;

    private void Start()
    {
        if (shakeOnStart)
            DoShake();
    }

    private void OnDestroy()
    {
        if (shakeOnDestroy)
            DoShake();
    }

    public void DoShake()
    {
        Pipe_CamShakes.AddCamShake(ShakeAtributes);
    }
}
