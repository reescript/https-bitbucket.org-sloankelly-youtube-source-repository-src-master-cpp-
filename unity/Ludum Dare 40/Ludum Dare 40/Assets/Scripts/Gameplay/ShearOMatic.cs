using UnityEngine;

[RequireComponent(typeof(SwapSpriteOnTrigger))]
public class ShearOMatic : MonoBehaviour
{
    private SwapSpriteOnTrigger swapSprite;

    private void Awake()
    {
        swapSprite = GetComponent<SwapSpriteOnTrigger>();
        swapSprite.filter = (c, e) =>
        {
            return c.CompareTag("TractorBeam");
        };
    }
}
