using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SwapSpriteOnTrigger : MonoBehaviour
{
    private SpriteRenderer sprite;

    public Sprite[] sprites;

    public Func<GameObject, bool, bool> filter = (c, e) => { return true; };

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (filter(collision.gameObject, true))
        {
            sprite.sprite = sprites[1];
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (filter(collision.gameObject, false))
        {
            sprite.sprite = sprites[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (filter(collision.gameObject, true))
        {
            sprite.sprite = sprites[1];
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (filter(collision.gameObject, false))
        {
            sprite.sprite = sprites[0];
        }
    }
}
