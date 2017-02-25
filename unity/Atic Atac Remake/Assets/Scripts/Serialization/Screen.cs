using System;
using System.Collections.Generic;

/// <summary>
/// Screen data.
/// </summary>
[Serializable]
public class Screen
{
    public int screenShape;
    public AAColour colour;
    public List<BackgroundObject> objects;
}
