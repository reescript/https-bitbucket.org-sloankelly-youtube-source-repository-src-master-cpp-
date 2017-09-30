using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ComponentModel;
using LINQtoCSV;
using UnityEngine;

public class CsvExampleObjectSpawner : MonoBehaviour
{
    private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    [Tooltip("The prefabs for each object")]
    public GameObject[] objects;

    [Tooltip("The file that contains the objects to be spawned in this scene")]
    public TextAsset sceneDescriptor;

    [Tooltip("The level that the player is currently on")]
    public string levelName = "red";

    void Awake()
    {
        foreach (var obj in objects)
        {
            prefabs[obj.name] = obj;
        }
    }

    void Start()
    {
		CsvFileDescription inputFileDescription = new CsvFileDescription
		{
			SeparatorChar = ',',
			FirstLineHasColumnNames = true
		};

        using (var ms = new MemoryStream(sceneDescriptor.text.Length))
        {
            using (var txtWriter = new StreamWriter(ms))
            {
                using (var txtReader = new StreamReader (ms))
                {
                    txtWriter.Write(sceneDescriptor.text);
                    txtWriter.Flush();
                    ms.Seek(0, SeekOrigin.Begin);

                    // Read the data from the CSV file
                    CsvContext cc = new CsvContext();
                    cc.Read<CsvSceneObject>(txtReader, inputFileDescription)
                      .Where(so => so.LevelName.Equals(levelName))
                      .ToList()
                      .ForEach(so =>
                       {
                           // Create an instance of the named prefab (don't forget give the instance the name and postion)
                           GameObject copy = Instantiate(prefabs[so.PrefabName]);
                           copy.name = so.InstanceName;
                           copy.transform.position = so.Position;
                           copy.SetActive(so.InitiallyVisible == Visibility.Yes);
                       });
                }
            }
        }
    }
}
