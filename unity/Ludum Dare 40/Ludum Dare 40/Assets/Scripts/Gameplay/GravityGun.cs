using UnityEngine;

public class GravityGun : MonoBehaviour
{
    private static readonly float MinimumFireValue = -0.8f;

    private TractorBeam tractorBeam;

    bool fireWasHeld = false;
    public Transform pivot;
    float speed = 10f;

    private void Start()
    {
        tractorBeam = GetComponentInChildren<TractorBeam>();
    }

    void Update ()
    {

        bool currentFiring = Input.GetAxis("Fire") < MinimumFireValue;
        pivot.gameObject.SetActive(currentFiring);
        
        float x = Input.GetAxis("Mouse_Look_X");
        float y = -Input.GetAxis("Mouse_Look_Y");
        
        var nextAngles = new Vector3(0, 0, Mathf.Atan2(x, y) * -180 / Mathf.PI);
        pivot.eulerAngles = nextAngles;

        if (!currentFiring && fireWasHeld) tractorBeam.Release(pivot.eulerAngles, speed);

        fireWasHeld = currentFiring;
    }
}
