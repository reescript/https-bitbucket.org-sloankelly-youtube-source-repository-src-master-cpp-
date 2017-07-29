using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGenerator))]
[RequireComponent(typeof(BlockRenderer))]
[RequireComponent(typeof(PlayerStatistics))]
public class GameController : MonoBehaviour
{
    const int TREE_OXYGEN = 5;

    class VectorI2
    {
        public int X;
        public int Y;

        public VectorI2(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    PlayerStatistics stats;
    BlockRenderer blockRenderer;
    Dictionary<int, int> treeHealth = new Dictionary<int, int>();
    Dictionary<int, Action<int, int>> interactions = new Dictionary<int, Action<int, int>>();
    int[,] map;

    VectorI2 playerPos = new VectorI2(50, 45);

    public void MoveEast()
    {
        PerformMove(vec => new VectorI2(vec.X + 1, vec.Y));
    }

    public void MoveWest()
    {
        PerformMove(vec => new VectorI2(vec.X - 1, vec.Y));
    }

    public void MoveNorth()
    {
        PerformMove(vec => new VectorI2(vec.X, vec.Y + 1));
    }

    public void MoveSouth()
    {
        PerformMove(vec => new VectorI2(vec.X, vec.Y - 1));
    }

    public bool SquareIsValid(int x, int y)
    {
        if (x < 0 || y < 0 || x > 89 || y > 89) return false;
        return true;
    }

    void Awake()
    {
        CreateInteractions();
    }

    void Start()
    {
        var mapGenerator = GetComponent<MapGenerator>();
        blockRenderer = GetComponent<BlockRenderer>();
        stats = GetComponent<PlayerStatistics>();
        map = mapGenerator.Generate(90, 90);

        blockRenderer.Initialize(SquareIsValid, map, playerPos.X, playerPos.Y);

        StartCoroutine(ReduceOxygen());
    }

    private void PerformMove(Func<VectorI2, VectorI2> desiredPosition)
    {
        // TODO: Moving in direction of baddie - shoot first ask questions later

        if (MoveIsValid(desiredPosition))
        {
            playerPos = desiredPosition(playerPos);
            blockRenderer.SetCurrentPosition(playerPos.X, playerPos.Y);
            stats.Steps++;
        }
        else 
        {
            var desiredPos = desiredPosition(playerPos);
            var blockId = map[desiredPos.X, desiredPos.Y];

            Action<int, int> interact = null;
            if (interactions.TryGetValue(blockId, out interact))
            {
                interact(desiredPos.X, desiredPos.Y);
            }

            blockRenderer.UpdateMap();
        }
    }

    private bool MoveIsValid(Func<VectorI2, VectorI2> func)
    {
        VectorI2 temp = func(playerPos);
        if (SquareIsValid(temp.X, temp.Y))
        {
            return map[temp.X, temp.Y] == 0;
        }

        return false;
    }

    private void CreateInteractions()
    {
        interactions[Constants.Objects.Tree] = (x, y) =>
        {
            int hashCode = Hash.GetHashCode(x, y);
            if (!treeHealth.ContainsKey(hashCode))
            {
                treeHealth[hashCode] = 2;
            }
            else
            {
                treeHealth[hashCode]--;
            }

            stats.Oxygen--;

            if (treeHealth[hashCode] == 0)
            {
                // Replace with floor
                map[x, y] = 0; // make it a floor
                stats.Oxygen += TREE_OXYGEN;
            }

            blockRenderer.SpawnExplosion(x, y);
        };
    }

    private IEnumerator ReduceOxygen()
    {
        while (stats.Oxygen >0)
        {
            yield return new WaitForSeconds(Constants.Energy.OxygenLoss);
            stats.Oxygen--;
        }
    }
}
