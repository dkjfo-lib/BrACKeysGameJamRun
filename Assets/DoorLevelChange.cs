using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLevelChange : MonoBehaviour
{
    LevelChanger levelChanger;

    void Start()
    {
        levelChanger = GetComponent<LevelChanger>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject == PlayerSinglton.thePlayer.gameObject)
        {
            levelChanger.ChangeScene();
        }
    }
}
