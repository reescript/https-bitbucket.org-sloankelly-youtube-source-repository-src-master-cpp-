using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    List<Pipework> pipePool;
    int index = -1;
    
    public GameObject pipePrefab;
    
    public int poolSize = 5;

    public float spawnTime = 4f;

    public float speed = 35;

    public bool isRunning = true;

    [HideInInspector]
    public bool hit = false;

    void Start()
    {
        pipePool = new List<Pipework>();
        for (int i = 0; i < poolSize; i++)
        {
            var pipe = (GameObject)Instantiate(pipePrefab, new Vector3(-400, 0, 0), Quaternion.identity);
            pipePool.Add(pipe.GetComponent<Pipework>());
        }

        StartCoroutine(SpawnPipe());
    }

    void Update()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var pipe = pipePool[i];
            pipe.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (pipe.hit)
            {
                hit = true;
                StopAllCoroutines();
            }
        }
    }

    IEnumerator SpawnPipe()
    {
        while (isRunning)
        {
            index++;
            index %= poolSize;

            var pipe = pipePool[index];

            float y = Random.Range(-15, 75);
            pipe.transform.position = new Vector3(100, y, 0);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
