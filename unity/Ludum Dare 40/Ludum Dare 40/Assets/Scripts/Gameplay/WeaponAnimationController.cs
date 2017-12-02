using UnityEngine;

public class WeaponAnimationController : MonoBehaviour
{
    private static readonly float MinimumFireValue = -0.8f;

    public Transform weaponAnimation;
	
	void Update ()
    {
        bool currentFiring = Input.GetAxis("Fire") < MinimumFireValue;
        weaponAnimation.gameObject.SetActive(currentFiring);
    }
}
