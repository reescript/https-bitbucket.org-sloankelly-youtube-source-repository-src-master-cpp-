using System.Collections;
using UnityEngine;

public class WoolSpawner : MonoBehaviour
{
    public float time = 3f;

    public float radius = 5f;

    public GameObject woolPrefab;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);

        Vector3 point = Random.insideUnitCircle * radius;

        var wool = Instantiate(woolPrefab);
        wool.transform.position = transform.position + point;
        Destroy(this);
    }
}
