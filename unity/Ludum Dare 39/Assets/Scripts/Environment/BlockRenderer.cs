using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MapGenerator))]
public class BlockRenderer : MonoBehaviour
{
    const int TILE_SIZE = 108;

    List<SpriteRenderer> squares = new List<SpriteRenderer>();
    Dictionary<int, GameObject> explosionInstances = new Dictionary<int, GameObject>();
    Func<int, int, bool> SquareIsValid;
    Vector2 currentPosition;
    Texture2D miniMap;

    public Image miniMapImage;
    
    public Sprite[] blocks;

    public Sprite[] explosion;

    public Sprite[] battery;

    public int[,] map;

    public void SpawnExplosion(int x, int y)
    {
        SpawnObject(x, y, explosion);
    }

    public void SpawnBattery(int x, int y)
    {
        SpawnObject(x, y, battery);
    }

    public void SetCurrentPosition(int x, int y)
    {
        currentPosition = new Vector2(x, y);
        UpdateMap();
    }

    public void Initialize(Func<int, int, bool> squareValidation, int[,] theMap, int x, int y)
    {
        miniMap = new Texture2D(90, 90);
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

        UpdateMiniMap();

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

    private void UpdateMiniMap()
    {
        var data = miniMap.GetPixels();

        var floorColour = new Color(0.3f, 0.3f, 0.3f);

        for (int y = 0; y < 90; y++)
        {
            for (int x = 0; x < 90; x++)
            {
                var index = (y * 90) + x;

                switch(map[x, y])
                {
                    case Constants.Objects.Floor:
                        data[index] = floorColour;
                        break;
                    case Constants.Objects.WayBelow:
                        data[index] = Color.black;
                        break;
                    case Constants.Objects.Battery:
                        data[index] = Color.blue;
                        break;
                    case Constants.Objects.Tree:
                        data[index] = Color.green;
                        break;
                    case Constants.Objects.Radio:
                        data[index] = Color.white;
                        break;
                }

                if (x == currentPosition.x && y== currentPosition.y)
                {
                    data[index] = Color.red;
                }
            }
        }

        miniMap.SetPixels(data);
        miniMap.Apply();
        miniMapImage.sprite = Sprite.Create(miniMap, new Rect(0, 0, 90, 90), Vector2.zero);
    }
    
    private void SpawnObject(int x, int y, Sprite[] frames)
    {
        GameObject go = new GameObject();
        var sprite = go.AddComponent<SpriteRenderer>();
        sprite.sprite = explosion[0];
        sprite.sortingOrder = 50;
        go.SetActive(true);
        explosionInstances[Hash.GetHashCode(x, y)] = go;
        StartCoroutine(DoExplosion(go, x, y, frames));
    }

    private IEnumerator DoExplosion(GameObject go, int x, int y, Sprite[] frames)
    {
        var sprite = go.GetComponent<SpriteRenderer>();

        int frame = 0;
        while (frame < 3)
        {
            sprite.sprite = frames[frame];
            yield return new WaitForSeconds(0.06f);
            frame++;
        }

        Destroy(go);
        explosionInstances.Remove(Hash.GetHashCode(x, y));
    }
}
