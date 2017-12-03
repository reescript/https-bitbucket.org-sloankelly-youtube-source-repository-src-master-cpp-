using System.Collections;
using UnityEngine;

public class SheepFactory : MonoBehaviour
{
    int sheepCount = 0;

    public GameObject sheepPrefab;

    public float minSpeed = 2;

    public float speedRandom = 4;

    public float spawnTime = 2f;

    public System.Action SheepEnteredSheerOMatic = () => { };

    public int SheepCount { get { return sheepCount; } }

    private IEnumerator Start()
    {
        while (true)
        {
            var dolly = Instantiate(sheepPrefab);
            dolly.transform.position = transform.position;

            var sheep = dolly.GetComponent<Sheep>();
            sheep.SheepEnteredSheerOMatic = SheepEnteredSheerOMatic;
            sheep.speed = minSpeed + Random.value * speedRandom;

            sheepCount++;

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
