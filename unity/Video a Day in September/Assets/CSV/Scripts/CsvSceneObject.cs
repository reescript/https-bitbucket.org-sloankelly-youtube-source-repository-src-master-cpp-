using System.ComponentModel;
using UnityEngine;

public class CsvSceneObject
{
    public string LevelName { get; set; }

    public string PrefabName { get; set; }

    public string InstanceName { get; set; }

    [TypeConverter(typeof(StringToVector3Converter))]
    public Vector3 Position { get; set; }

    [TypeConverter(typeof(StringToColorConverter))]
    public Color Colour { get; set; }

    public Visibility InitiallyVisible { get; set; }
}
