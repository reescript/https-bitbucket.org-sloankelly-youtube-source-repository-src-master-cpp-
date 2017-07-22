using UnityEngine;
using Com.SloanKelly.ZXSpectrum;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(RoomStore))]
[RequireComponent(typeof(SpectrumScreen))]
[RequireComponent(typeof(RoomRenderer))]
public class GameController : MonoBehaviour
{
    // Private member fields

    int score = 0;
    int hiScore = 100;
    const string ScoreFormat = "High Score {0:000000}   Score {1:000000}";
    private RoomData roomData;
    private bool gameOver;

    List<Mob> mobs = new List<Mob>();

    // Public member fields

    [Tooltip("The room number (0-19")]
    public int roomId;
    
    IEnumerator Start()
    {
        var store = GetComponent<RoomStore>();
        var roomRenderer = GetComponent<RoomRenderer>();

        while (!store.IsReady)
        {
            yield return null;
        }

        roomData = store.Rooms[roomId];

        roomData.HorizontalGuardians.ForEach(g => mobs.Add(new Mob(g)));

        StartCoroutine(DrawScreen(roomRenderer, roomData));
        StartCoroutine(LoseAir(roomData));

        if ((roomId>=0 && roomId <=6) || roomId==9 || roomId==15)
        {
            StartCoroutine(BidirectionalSprites());
        }
    }

    IEnumerator DrawScreen(RoomRenderer roomRenderer, RoomData roomData)
    {
        while (!gameOver)
        {
            roomRenderer.DrawScreen(roomData, mobs, string.Format(ScoreFormat, hiScore, score));
            yield return null;
        }
    }

    IEnumerator LoseAir(RoomData roomData)
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(1);

            roomData.AirSupply.Tip = (byte)(roomData.AirSupply.Tip << 1);
            roomData.AirSupply.Tip = (byte)(roomData.AirSupply.Tip & 0xff);

            if (roomData.AirSupply.Tip == 0)
            {
                roomData.AirSupply.Length--;
                roomData.AirSupply.Tip = 255;

                gameOver = roomData.AirSupply.Length < 0;
            }
        }
    }

    IEnumerator BidirectionalSprites()
    {
        foreach (var m in mobs)
        {
            m.FrameDirection = m.Frame < 4 ? 1 : -1;
        }

        while (!gameOver)
        {
            yield return new WaitForSeconds(0.1f);

            foreach (var m in mobs)
            {
                m.Frame += m.FrameDirection;
                
                // is the sprite heading left to right?
                if (m.FrameDirection > 0 && m.Frame > 3)
                {
                    m.Frame = 0;
                    m.X += m.FrameDirection;

                    // Have they reached the end?
                    if (m.X > m.Right)
                    {
                        m.X = m.Right;
                        m.FrameDirection *= -1;
                        m.Frame = 7;
                    }
                }
                
                // the sprite is heading right to left
                if (m.FrameDirection < 0 && m.Frame < 4)
                {
                    m.Frame = 7;
                    m.X += m.FrameDirection;

                    if (m.X < m.Left)
                    {
                        m.X = m.Left;
                        m.FrameDirection *= -1;
                        m.Frame = 0;
                    }
                }
            }
        }
    }
}
