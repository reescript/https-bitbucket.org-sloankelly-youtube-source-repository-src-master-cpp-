using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    const int FLOOR = 0;
    const int OBSTACLE = 1;

    public Texture2D map;

    internal int[,] Generate(int columns, int rows)
    {
        int[,] generatedMap = new int[columns, rows];

        Color[] clrs = map.GetPixels();

        for (int i = 0; i < clrs.Length; i++)
        {
            int x = i % columns;
            int y = i / columns;

            generatedMap[x, y] = clrs[i].r == 0 && clrs[i].g == 0 ? OBSTACLE : GetFloorOrRandomObject();
        }
        return generatedMap;
    }

    private int GetFloorOrRandomObject()
    {
        var num = Random.Range(0.0f, 1.0f);

        if (num > 0.99f)
            return Constants.Objects.Battery;

        if (num > 0.9f)
            return Constants.Objects.Tree;

        return FLOOR;
    }
}
