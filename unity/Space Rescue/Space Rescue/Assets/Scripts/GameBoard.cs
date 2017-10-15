using UnityEngine;

public class GameBoard : MonoBehaviour 
{
    bool[,] grid = new bool[5, 6];

    public bool GetValueAt(int x, int y)
    {
        return grid[x, y];
    }

    public void SetValueAt(int x, int y, bool newValue = true)
    {
        grid[x, y] = newValue;
    }

    public void Clear()
    {
        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                grid[x, y] = false;
            }
        }
    }

    void Awake()
    {
        Clear();
    }
}
