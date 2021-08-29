using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnSceneStart : MonoBehaviour
{
    public Pipe_Weapon Pipe_Weapon;

    void Awake()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "tutorial")
        {
            Pipe_Weapon.gameWeapons[0].unlocked = true;
            Pipe_Weapon.gameWeapons[1].unlocked = false;
            Pipe_Weapon.gameWeapons[2].unlocked = false;
        }
        if (sceneName == "main")
        {
            Pipe_Weapon.gameWeapons[0].unlocked = true;
            Pipe_Weapon.gameWeapons[1].unlocked = true;
            Pipe_Weapon.gameWeapons[2].unlocked = false;
        }
        if (sceneName == "boss")
        {
            Pipe_Weapon.gameWeapons[0].unlocked = true;
            Pipe_Weapon.gameWeapons[1].unlocked = true;
            Pipe_Weapon.gameWeapons[2].unlocked = true;
        }
    }
}
