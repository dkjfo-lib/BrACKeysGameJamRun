using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public float shakeLength = .05f;

    public Vector3 CurrentDisplacement { get; private set; }

    private void Start()
    {
        Shake(.5f, 6, 100);
    }

    public void Shake(float amplitude, float duration, float dencity)
    {
        StartCoroutine(PerformShake(amplitude, duration, dencity));
    }

    IEnumerator PerformShake(float amplitude, float duration, float dencity)
    {
        while (true)
        {
            var newDisplacement = new Vector3(Random.Range(-amplitude, amplitude), Random.Range(-amplitude, amplitude));
            CurrentDisplacement += newDisplacement;
            yield return new WaitForSeconds(shakeLength);
            CurrentDisplacement = Vector2.zero;
            yield return new WaitForSeconds(Mathf.Max(1f / dencity, shakeLength));
        }
    }
}
