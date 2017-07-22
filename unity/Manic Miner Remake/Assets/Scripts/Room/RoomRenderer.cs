using Com.SloanKelly.ZXSpectrum;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RoomStore))]
[RequireComponent(typeof(SpectrumScreen))]
public class RoomRenderer : MonoBehaviour
{
    // TODO: HACK: Adding this variable in because it's convenient for now
    // This should be replaced with another class? Something else at least
    int airHead = 252;
    int airSupplyLength = 27;

    // HACK: TODO: Scores don't live here!
    // The format can stay tho
    int score = 0;
    int hiScore = 100;
    const string ScoreFormat = "High Score {0:000000}   Score {1:000000}";

    RoomStore store;

    SpectrumScreen screen;

    public int roomId; // a value between 0 and 19 for the twenty rooms

    public Transform target;

    [Tooltip("The material that has the pixel perfect check box")]
    public Material pixelPerfect;

    public Sprite minerStartTemp; // TODO: REMOVE THIS FOR PRODUCTION

    public Sprite roomKeyTemp;
    
    IEnumerator Start()
    {
        store = GetComponent<RoomStore>();

        while (!store.IsReady)
        {
            yield return null;
        }
        
        screen = GetComponent<SpectrumScreen>();
        var sr = target.GetComponent<SpriteRenderer>();
        sr.sprite = Sprite.Create(screen.Texture, new Rect(0, 0, 256, 192), new Vector2(0, 1), 1f);

        RoomData data = store.Rooms[roomId];

        airHead = data.AirSupply.Tip;
        airSupplyLength = data.AirSupply.Length;

        StartCoroutine(DrawScreen(data));
        StartCoroutine(LoseAir());
    }

    IEnumerator DrawScreen(RoomData data)
    {
        while (airSupplyLength > 0)
        {
            screen.Clear(7, 0, false);

            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    int attr = data.Attributes[y * 32 + x];
                    if (attr != 0)
                    {
                        // HACK: SOmething v. wrong with room #19
                        if (!data.Blocks.ContainsKey(attr)) continue;

                        int ink = attr.GetInk();
                        int paper = attr.GetPaper();
                        bool bright = attr.IsBright();
                        bool flashing = attr.IsFlashing();

                        screen.SetAttribute(x, y, ink, paper, bright, flashing);
                        screen.DrawSprite(x, y, 1, 1, data.Blocks[attr]);
                    }
                }
            }

            for (int py = 0; py < 2; py++)
            {
                for (int px = 0; px < 2; px++)
                {
                    screen.SetAttribute(data.Portal.X + px, data.Portal.Y + py, data.Portal.Attr);
                }
            }

            foreach (var key in data.RoomKeys)
            {
                screen.SetAttribute(key.Position.X, key.Position.Y, 2, 0, true, false);
                screen.DrawSprite(key.Position.X, key.Position.Y, 1, 1, data.KeyShape);
            }

            screen.RowOrderSprite();
            screen.DrawSprite(data.Portal.X, data.Portal.Y, 2, 2, data.Portal.Shape);

            // Draw the room title
            for (int x = 0; x < 32; x++)
                screen.SetAttribute(x, 16, 0, 6);

            screen.PrintMessage(0, 16, data.RoomName);

            // Draw the air supply
            for (int x = 0; x < 10; x++)
                screen.SetAttribute(x, 17, 7, 2);

            for (int x = 10; x < 32; x++)
                screen.SetAttribute(x, 17, 7, 4);

            byte[] airBlock = new byte[] { 0, 0, 255, 255, 255, 255, 0, 0 };

            for (int x = 0; x < airSupplyLength; x++)
                screen.DrawSprite(x + 4, 17, 1, 1, airBlock);

            byte[] airTipBlock = new byte[] { 0, 0, (byte)airHead, (byte)airHead, (byte)airHead, (byte)airHead, 0, 0 };
            screen.DrawSprite(4 + airSupplyLength, 17, 1, 1, airTipBlock);

            screen.PrintMessage(0, 17, "AIR");

            // Draw the score
            for (int x = 0; x < 32; x++)
                screen.SetAttribute(x, 19, 6, 0);
            screen.PrintMessage(0, 19, string.Format(ScoreFormat, hiScore, score));

            yield return null;
        }
    }

    IEnumerator LoseAir() // HACK
    {
        while (airSupplyLength > 0)
        {
            yield return new WaitForSeconds(1);

            airHead = airHead << 1;
            airHead = airHead & 0xff;

            if (airHead == 0)
            {
                airSupplyLength--;
                airHead = 255;
            }
        }
    }
}

