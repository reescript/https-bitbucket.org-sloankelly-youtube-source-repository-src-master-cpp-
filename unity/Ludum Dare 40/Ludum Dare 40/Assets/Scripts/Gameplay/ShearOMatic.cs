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
            if (!c.CompareTag("Sheep")) return false;

            var sheep = c.GetComponent<Sheep>();
            return sheep.sheepState == SheepState.HeldInTractorBeam;
        };
    }
}
