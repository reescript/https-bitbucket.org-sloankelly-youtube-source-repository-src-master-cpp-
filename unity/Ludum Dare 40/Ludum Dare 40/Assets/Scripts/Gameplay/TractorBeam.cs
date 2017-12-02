using System.Collections;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    float speed = 8f;
    Sheep currentSheep;
    bool droppingOff = false;

    public void Release(Vector3 direction, float speed)
    {
        if (currentSheep == null) return;

        if (droppingOff)
        {
            Drop();
        }
        else
        {
            Throw(direction, speed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SheerOMatic"))
        {
            droppingOff = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SheerOMatic"))
        {
            droppingOff = true;
        }
        else
        {
            if (!collision.CompareTag("Sheep")) return;
            if (currentSheep != null) return;

            CaptureSheep(collision.gameObject.GetComponent<Sheep>());
        }
    }

    private void CaptureSheep(Sheep sheep)
    {
        currentSheep = sheep;
        sheep.Captured();
        StartCoroutine(Capture(sheep));
    }

    private IEnumerator Capture(Sheep sheep)
    {
        // TODO: Make this animated

        sheep.transform.SetParent(transform);
        sheep.transform.localPosition = Vector3.zero;

        yield break;

        //Vector3 target = transform.position;
        //float t = 0;
        //while (t != 1)
        //{
        //    var current = Vector3.Lerp(sheep.transform.localPosition, target, t);
        //    t += Time.deltaTime / speed;
        //    sheep.transform.localPosition = current;
        //    yield return null;
        //}

        //var finalPos = Vector3.Lerp(sheep.transform.position, target, 1f);
        //sheep.transform.position = finalPos;
    }
    
    private void Drop()
    {
        if (currentSheep == null) return;

        currentSheep.GetSheared();
        currentSheep.transform.SetParent(null);
        currentSheep = null;
    }

    private void Throw(Vector3 direction, float speed)
    {
        if (currentSheep == null) return;

        currentSheep.Fling(direction, speed);
        currentSheep.transform.SetParent(null);
        currentSheep = null;
    }
}
