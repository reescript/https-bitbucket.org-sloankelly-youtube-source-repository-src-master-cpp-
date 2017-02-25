using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loads the map data into memory and makes it available for the game controller.
/// </summary>
public class MapLoader : MonoBehaviour
{
    #region Public member fields

    [Tooltip("The location of the map data in the Resources folder")]
    public string mapFile = "completemap";

    #endregion

    #region Public properties

    /// <summary>
    /// The screen data.
    /// </summary>
    public Dictionary<int, Screen> Screens { get; private set; }

    /// <summary>
    /// The data has been loaded
    /// </summary>
    public bool IsLoaded { get; private set; }

    #endregion

    #region Unity messages

    /// <summary>
    /// Load the map data into memory.
    /// </summary>
    void Awake()
    {
        // Create our cache of screen data
        Screens = new Dictionary<int, Screen>();

        // Load the data from the text file and create the map data object from the JSON
        TextAsset txtAsset = Resources.Load<TextAsset>(mapFile);
        var mapData = JsonUtility.FromJson<MapData>(txtAsset.text);

        // Add each room to the cache
        foreach (var screen in mapData.screens)
        {
            Screens[screen.screenId] = screen;
        }

        IsLoaded = true;
    }

    #endregion
}
