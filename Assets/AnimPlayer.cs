using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour
{
    public Sprite[] Sprites;

    SpriteRenderer SpriteRenderer;
    PlayerShoot aim;

    void Start()
    {
        aim = transform.parent.GetComponentInChildren<PlayerShoot>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (aim.transform.right.y > 0)
            SpriteRenderer.sprite = Sprites[0];
        if (aim.transform.right.y < 0)
            SpriteRenderer.sprite = Sprites[1];
    }
}
