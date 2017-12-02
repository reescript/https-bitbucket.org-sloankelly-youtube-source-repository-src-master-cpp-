using System.Collections;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    float speed = 8f;
    Sheep currentSheep;

    public void Drop()
    {
        if (currentSheep == null) return;

        currentSheep.sheepState = SheepState.MovingAround;
        currentSheep.transform.SetParent(null);
        currentSheep = null;
    }

    public void Throw(Vector3 direction, float speed)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Sheep")) return;
        if (currentSheep != null) return;

        CaptureSheep(collision.gameObject.GetComponent<Sheep>());
    }

    private void CaptureSheep(Sheep sheep)
    {
        currentSheep = sheep;
        sheep.sheepState = SheepState.HeldInTractorBeam;
        StartCoroutine(Capture(sheep));
    }

    private IEnumerator Capture(Sheep sheep)
    {
        sheep.transform.SetParent(transform);

        Vector3 target = transform.position;
        float t = 0;
        while (t != 1)
        {
            var current = Vector3.Lerp(sheep.transform.localPosition, target, t);
            t += Time.deltaTime / speed;
            sheep.transform.localPosition = current;
            yield return null;
        }

        var finalPos = Vector3.Lerp(sheep.transform.position, target, 1f);
        sheep.transform.position = finalPos;
    }
}
