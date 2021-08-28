using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnim : MonoBehaviour
{
    public bool Moves = false;
    [Space]
    public Sprite sIdle;
    public Sprite sMoves;

    SpriteRenderer Sprite;
    IBotMovement BotMovement;

    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        BotMovement = GetComponentInParent<IBotMovement>();
        StartCoroutine(UpdateAnimator_Moves());
    }

    IEnumerator UpdateAnimator_Moves()
    {
        while (true)
        {
            Moves = BotMovement.Moves;
            if (Moves == false)
                Sprite.sprite = sIdle;
            if (Moves == true)
                Sprite.sprite = sMoves;
            yield return new WaitUntil(() => BotMovement.Moves != Moves);
        }
    }
}
