using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OnDestroyChangeLevel : MonoBehaviour
{
    public float timeDelay = 2f;
    public GameObject[] Bots;

    LevelChanger LevelChanger;

    void Start()
    {
        LevelChanger = GetComponent<LevelChanger>();
        StartCoroutine(WaitTillAllDead());
    }

    IEnumerator WaitTillAllDead()
    {
        yield return new WaitUntil(() => Bots.All(s => s == null));
        yield return new WaitForSeconds(timeDelay);
        LevelChanger.ChangeScene();
    }
}
