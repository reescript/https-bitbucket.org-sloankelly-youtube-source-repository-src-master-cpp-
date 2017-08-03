using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGenerator))]
[RequireComponent(typeof(BlockRenderer))]
[RequireComponent(typeof(PlayerStatistics))]
[RequireComponent(typeof(SpawnSerializer))]
[RequireComponent(typeof(GameCanvasController))]
[RequireComponent(typeof(SoundController))]
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
    SoundController sounds;
    Dictionary<int, int> treeHealth = new Dictionary<int, int>();
    Dictionary<int, Action<int, int>> interactions = new Dictionary<int, Action<int, int>>();
    int[,] map;
    bool paused;

    VectorI2 playerPos = new VectorI2(50, 45);

    public GameObject inGameMenu;
    public RescueTimeCounter counter;

    public void TogglePause()
    {
        if (paused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        inGameMenu.SetActive(true);
        counter.Pause(true);
        paused = true;
    }

    public void ResumeGame()
    {
        inGameMenu.SetActive(false);
        counter.Pause(false);
        paused = false;
    }

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
        StartCoroutine(KeepEyeOnTimer());
    }
    
    void Start()
    {
        var mapGenerator = GetComponent<MapGenerator>();
        blockRenderer = GetComponent<BlockRenderer>();
        stats = GetComponent<PlayerStatistics>();
        spawnSerializer = GetComponent<SpawnSerializer>();
        canvasController = GetComponent<GameCanvasController>();
        sounds = GetComponent<SoundController>();
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
        if (paused) return;

        if (MoveIsValid(desiredPosition))
        {
            playerPos = desiredPosition(playerPos);

            if (map[playerPos.X, playerPos.Y] == Constants.Objects.Battery)
            {
                stats.Batteries++;
                map[playerPos.X, playerPos.Y] = Constants.Objects.Floor;
                sounds.PickUpBattery();
                blockRenderer.SpawnBattery(playerPos.X, playerPos.Y);
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

            sounds.FireWeapon();

            if (treeHealth[hashCode] == 0)
            {
                // Replace with floor
                map[x, y] = Constants.Objects.Floor;
                stats.Oxygen += TREE_OXYGEN;
                sounds.TreeDestroyed();
            }

            blockRenderer.SpawnExplosion(x, y);
        };

        interactions[Constants.Objects.Radio] = (x, y) =>
        {
            if (stats.Batteries == 0) return;

            stats.Batteries--;
            stats.Radio += Constants.Energy.Volts;
            blockRenderer.SpawnBattery(x, y);
            sounds.PowerUpRadio();

            if (stats.Radio == 100)
            {
                sounds.RadioFull();
            }
        };
    }

    private IEnumerator ReduceOxygen()
    {
        while (stats.Oxygen > 0)
        {
            if (paused)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(Constants.Energy.OxygenLossDelay);
                stats.Oxygen--;
            }
        }

        canvasController.DoOxygenDeath();
        sounds.MissionFailed();
    }

    private IEnumerator RunDownRadio()
    {
        while (stats.Oxygen > 0)
        {
            if (paused)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(Constants.Energy.BatteryLossDelay);
                stats.Radio -= Constants.Energy.VoltageLoss;
            }
        }
    }

    private IEnumerator KeepEyeOnTimer()
    {
        while (counter.Running)
        {
            yield return null;
        }

        if (stats.Radio < 40)
        {
            canvasController.DoNoRescue();
            sounds.MissionFailed();
        }
        else
        {
            canvasController.DoRescued();
        }
    }
}
