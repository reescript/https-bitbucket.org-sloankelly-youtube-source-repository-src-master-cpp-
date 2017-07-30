using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGenerator))]
[RequireComponent(typeof(BlockRenderer))]
[RequireComponent(typeof(PlayerStatistics))]
[RequireComponent(typeof(SpawnSerializer))]
[RequireComponent(typeof(GameCanvasController))]
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
    SpawnSerializer spawnSerializer;
    GameCanvasController canvasController;
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
        spawnSerializer = GetComponent<SpawnSerializer>();
        canvasController = GetComponent<GameCanvasController>();
        map = mapGenerator.Generate(90, 90);

        Func<Spawn, VectorI2> GetStart = (s) =>
        {
            int rndId = UnityEngine.Random.Range(0, s.spawnPoints.Count);
            SpawnPoint sp = s.spawnPoints[rndId];

            var vec = new VectorI2((int)sp.playerSpawn.x, (int)sp.playerSpawn.y);
            map[vec.X, vec.Y] = Constants.Objects.Floor;

            var vecRadio = new VectorI2((int)sp.radioSpawn.x, (int)sp.radioSpawn.y);
            map[vecRadio.X, vecRadio.Y] = Constants.Objects.Radio;

            return vec;
        };

        playerPos = GetStart(spawnSerializer.GetSpawn());

        blockRenderer.Initialize(SquareIsValid, map, playerPos.X, playerPos.Y);

        StartCoroutine(ReduceOxygen());
        StartCoroutine(RunDownRadio());
    }

    private void PerformMove(Func<VectorI2, VectorI2> desiredPosition)
    {
        // TODO: Moving in direction of baddie - shoot first ask questions later

        if (MoveIsValid(desiredPosition))
        {
            playerPos = desiredPosition(playerPos);

            if (map[playerPos.X, playerPos.Y] == Constants.Objects.Battery)
            {
                stats.Batteries++;
                map[playerPos.X, playerPos.Y] = Constants.Objects.Floor;
            }

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
            return map[temp.X, temp.Y] == Constants.Objects.Floor || 
                   (map[temp.X, temp.Y] == Constants.Objects.Battery && stats.Batteries < Constants.Energy.MaxCarryBattery);
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
                map[x, y] = Constants.Objects.Floor;
                stats.Oxygen += TREE_OXYGEN;
            }

            blockRenderer.SpawnExplosion(x, y);
        };

        interactions[Constants.Objects.Radio] = (x, y) =>
        {
            if (stats.Batteries == 0) return;

            stats.Batteries--;
            stats.Radio += Constants.Energy.Volts;
            blockRenderer.SpawnBattery(x, y);
        };
    }

    private IEnumerator ReduceOxygen()
    {
        while (stats.Oxygen > 0)
        {
            yield return new WaitForSeconds(Constants.Energy.OxygenLossDelay);
            stats.Oxygen--;
        }

        canvasController.DoOxygenDeath();
    }

    private IEnumerator RunDownRadio()
    {
        while (stats.Oxygen > 0)
        {
            yield return new WaitForSeconds(Constants.Energy.BatteryLossDelay);
            stats.Radio -= Constants.Energy.VoltageLoss;
        }
    }
}
