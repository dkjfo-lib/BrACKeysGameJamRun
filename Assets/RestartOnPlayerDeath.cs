using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnPlayerDeath : MonoBehaviour
{
    public float timeDelay = 2f;

    void Start()
    {
        StartCoroutine(Monitor());
    }

    private IEnumerator Monitor()
    {
        while (true)
        {
            yield return new WaitUntil(() => PlayerSinglton.thePlayer == null);
            yield return new WaitForSeconds(timeDelay);
            var currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
            yield return new WaitForSeconds(timeDelay);
        }
    }
}
