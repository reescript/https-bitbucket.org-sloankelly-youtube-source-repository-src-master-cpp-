using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    private int sheepSheered;

    public SheepFactory sheepFactory;

    public Transform woolSpawnPoint;

    public GameObject woolPrefab;

    public int SheepSheered { get { return sheepSheered; } }

    private void Awake()
    {
        sheepFactory.SheepEnteredSheerOMatic = () => StartSheeringTheSheep();
    }

    public void StartSheeringTheSheep()
    {
        sheepSheered++;

        // TODO: Update radius and time to be fields 

        var woolSpawner = woolSpawnPoint.gameObject.AddComponent<WoolSpawner>();
        woolSpawner.radius = 0.5f;
        woolSpawner.woolPrefab = woolPrefab;
        woolSpawner.time = 3;
    }
}
