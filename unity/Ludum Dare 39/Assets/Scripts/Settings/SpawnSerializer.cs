using UnityEngine;

public class SpawnSerializer : MonoBehaviour
{
    public string file = "spawnpoints";

    public Spawn GetSpawn()
    {
        var textAsset = Resources.Load<TextAsset>(file);
        return JsonUtility.FromJson<Spawn>(textAsset.text);
    }

}
