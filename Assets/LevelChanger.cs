using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public string nextSceneName;

    public void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
