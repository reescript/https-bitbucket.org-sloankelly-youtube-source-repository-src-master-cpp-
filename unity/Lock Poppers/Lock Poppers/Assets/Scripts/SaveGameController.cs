using System.IO;
using UnityEngine;

public class SaveGameController : MonoBehaviour
{
    private string saveFileLocation;

    public bool IsReady { get; private set; }

    public int CurrentLevel { get; private set; }

    void Awake()
    {
        saveFileLocation = Path.Combine(Application.persistentDataPath, "savegame.json");

        if (File.Exists(saveFileLocation))
        {
            var json = File.ReadAllText(saveFileLocation);
            var saveGame = JsonUtility.FromJson<SaveGame>(json);
            CurrentLevel = saveGame.currentLevel;
        }
        else
        {
            CurrentLevel = 1;
        }

        IsReady = true;
    }

    public void SaveProgress(int level)
    {
        var saveGame = new SaveGame() { currentLevel = level };
        var json = JsonUtility.ToJson(saveGame);

        File.WriteAllText(saveFileLocation, json);
    }
}
