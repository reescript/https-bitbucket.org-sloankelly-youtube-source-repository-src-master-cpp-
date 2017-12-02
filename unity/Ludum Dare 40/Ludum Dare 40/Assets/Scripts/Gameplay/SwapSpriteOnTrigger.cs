using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SwapSpriteOnTrigger : MonoBehaviour
{
    private SpriteRenderer sprite;

    public Sprite[] sprites;

    public Func<Collider2D, bool, bool> filter = (c, e) => { return true; };

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (filter(collision, true))
        {
            sprite.sprite = sprites[1];
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (filter(collision, false))
        {
            sprite.sprite = sprites[0];
        }
    }
}
