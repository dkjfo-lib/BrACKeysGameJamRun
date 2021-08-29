using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
    public GameObject[] itemsToSpawn;
    public Vector2 localOffset;

    bool isQuitting = false;

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void OnDestroy()
    {
        if (isQuitting) return;

        Vector3 position = transform.position +
            transform.right * localOffset.x +
            transform.up * localOffset.y;
        foreach (var item in itemsToSpawn)
        {
            Instantiate(item, position, Quaternion.identity);
        }
    }
}
