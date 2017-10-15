using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Pianola : MonoBehaviour
{
    private List<PianolaObject> objects = new List<PianolaObject>();

    private string[] mapData;

    private int currentLine = -1;

	private bool isRunning = false;
    
    [Tooltip("The movement speed (in seconds) of the debris")]
    public float speed = 1f;

    [Tooltip("The list of files that contains the maps")]
    public TextAsset[] mapFiles;

    [Tooltip("The current map index")]
    public int currentMapIndex = 0;

	public GameController gameController;
	public GameBoard gameBoard;

    public void Reset()
    {
        isRunning = false;

        foreach (var obj in objects)
        {
            if (obj)
            {
                obj.StopAllCoroutines();
                Destroy(obj);
            }
        }

        StopAllCoroutines();
        objects.Clear();

        mapData = null;
    }

    public void StartPianola()
    {
        isRunning = true;
        StartCoroutine(RunThePianola());
    }

    IEnumerator RunThePianola()
    {
        while (true)
        {
            if (!isRunning)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(speed);

                if (mapData == null)
                {
                    mapData = mapFiles[currentMapIndex].text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    currentLine = mapData.Length - 1;
                }

                string line = mapData[currentLine];

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1')
                    {
                        var obj = PianolaObjectFactory.Create(gameObject, i, speed, gameBoard, gameController);
                        objects.Add(obj);
                    }
                }

                currentLine--;
                if (currentLine < 0)
                {
                    currentLine = mapData.Length - 1;
                }
            }
        }
    }
}
