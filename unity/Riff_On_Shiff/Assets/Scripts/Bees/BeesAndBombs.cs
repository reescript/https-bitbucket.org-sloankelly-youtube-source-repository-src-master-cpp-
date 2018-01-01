// Author               : Sloan Kelly
// Purpose              : Emulate GIF by 'Bees and Bombs'

// Original Code        : Daniel Shiffman
//                        http://codingtra.in
//                        http://patreon.com/codingtrain

// Coding Train Video   : https://youtu.be/H81Tdrmz2LA
// Bees and Bombs GIF   : https://beesandbombs.tumblr.com/post/149654056864/cube-wave

// ********************************************************************************
// This code has been tidied up since the video.
// ********************************************************************************

using System.Collections.Generic;
using UnityEngine;

public class BeesAndBombs : MonoBehaviour
{
    List<Oscillator> oscillators = new List<Oscillator>();

    float angle = 0;
    float maxD;

    public int rows = 24;
    public int cols = 24;

    public float speed = 5f;

    public GameObject cubePrefab;

    /// <summary>
    /// Create the cubes that will grow and shrink.
    /// </summary>
    void Awake()
    {
        maxD = rows;
        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < cols; x++)
            {
                var copy = Instantiate(cubePrefab, transform);
                var oscillator = copy.GetComponent<Oscillator>();

                copy.transform.position = new Vector3(x - cols / 2, 0, z - rows / 2);
                oscillators.Add(oscillator);
            }
        }
    }

    void Update()
    {
        int index = 0;

        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < cols; x++)
            {
                var oscillator = oscillators[index];
                oscillator.UpdateAngle(angle, maxD);
                index++;
            }
        }

        angle += speed * Time.deltaTime;
    }
}
