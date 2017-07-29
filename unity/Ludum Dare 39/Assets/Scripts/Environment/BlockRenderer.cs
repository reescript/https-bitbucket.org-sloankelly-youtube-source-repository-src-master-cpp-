using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGenerator))]
public class BlockRenderer : MonoBehaviour
{
    const int TILE_SIZE = 108;

    List<SpriteRenderer> squares = new List<SpriteRenderer>();
    Dictionary<int, GameObject> explosionInstances = new Dictionary<int, GameObject>();
    Func<int, int, bool> SquareIsValid;
    Vector2 currentPosition;
    

    // 0 - Space
    // 1 - Floor
    // 2 - Wall
    // 3 - Door ?? 

    public Sprite[] blocks;

    public Sprite[] explosion;

    public int[,] map;

    public void SpawnExplosion(int x, int y)
    {
        GameObject go = new GameObject();
        var sprite = go.AddComponent<SpriteRenderer>();
        sprite.sprite = explosion[0];
        sprite.sortingOrder = 50;
        go.SetActive(true);
        explosionInstances[Hash.GetHashCode(x, y)] = go;

        StartCoroutine(DoExplosion(go, x, y));
    }

    public void SetCurrentPosition(int x, int y)
    {
        currentPosition = new Vector2(x, y);
        UpdateMap();
    }

    public void Initialize(Func<int, int, bool> squareValidation, int[,] theMap, int x, int y)
    {
        SquareIsValid = squareValidation;
        map = theMap;
        SetCurrentPosition(x, y);
    }

    public void UpdateMap()
    {
        #region For DEBUG

        //for (int y = -45; y < 45; y++)
        //{
        //    for (int x = -45; x < 45; x++)
        //    {
        //        int spriteIndex = map[(int)currentPosition.x + x, (int)currentPosition.y + y];
        //        Sprite sprite = blocks[spriteIndex];

        //        Vector3 pos = new Vector3(x * sprite.texture.width, y * sprite.texture.height);
        //        GameObject go = new GameObject();
        //        go.transform.SetParent(transform);
        //        go.transform.position = pos;
        //        var render = go.AddComponent<SpriteRenderer>();
        //        render.sprite = sprite;
        //    }
        //}

        #endregion

        for (int y = -4; y < 5; y++)
        {
            for (int x = -4; x < 5; x++)
            {
                int xx = (int)currentPosition.x + x;
                int yy = (int)currentPosition.y + y;

                if (!SquareIsValid(xx, yy)) continue;

                int spriteIndex = map[xx, yy];
                Sprite sprite = blocks[spriteIndex];

                int x2 = x + 4;
                int y2 = y + 4;
                int index = (y2 * 9) + x2;
                squares[index].sprite = sprite;

                GameObject exploder = null;
                if (explosionInstances.TryGetValue(Hash.GetHashCode(xx, yy), out exploder))
                {
                    exploder.transform.position = squares[index].transform.position;
                }
            }
        }
    }

    void Awake()
    {
        Sprite sprite = blocks[0];

        for (int y = -4; y < 5; y++)
        {
            for (int x = -4; x < 5; x++)
            {
                Vector3 pos = new Vector3(x * TILE_SIZE, y * TILE_SIZE);
                GameObject go = new GameObject();
                go.transform.SetParent(transform);
                go.transform.position = pos;
                var render = go.AddComponent<SpriteRenderer>();
                render.sprite = sprite;

                squares.Add(render);
            }
        }
    }

    private IEnumerator DoExplosion(GameObject go, int x, int y)
    {
        var sprite = go.GetComponent<SpriteRenderer>();

        int frame = 0;
        while (frame < 3)
        {
            sprite.sprite = explosion[frame];
            yield return new WaitForSeconds(0.06f);
            frame++;
        }

        Destroy(go);
        explosionInstances.Remove(Hash.GetHashCode(x, y));
    }
}
