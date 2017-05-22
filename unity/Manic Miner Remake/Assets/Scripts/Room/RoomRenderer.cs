using Com.SloanKelly.ZXSpectrum;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RoomStore))]
[RequireComponent(typeof(SpectrumScreen))]
public class RoomRenderer : MonoBehaviour
{
    RoomStore store;

    SpectrumScreen screen;

    public int roomId; // a value between 0 and 19 for the twenty rooms

    public Transform target;

    [Tooltip("The material that has the pixel perfect check box")]
    public Material pixelPerfect;

    public Sprite minerStartTemp; // TODO: REMOVE THIS FOR PRODUCTION

    public Sprite roomKeyTemp;

    //// HACK: This does not belong here!
    //public CharScreen charScreen;
    
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
        
        //CellPoint pt = data.MinerWillyStart;
        ////AddSprite("Miner Willy Start", pt.ToVector3(), minerStartTemp);

        //foreach (var key in data.RoomKeys)
        //{
        //    //AddSprite("Key", key.Position.ToVector3(), roomKeyTemp);
        //}

        //// HACK: This does not belong here!
        //charScreen.PrintAt(data.RoomName, 0, 16);
        //charScreen.ApplyText();
    }

    //protected void AddSprite(string name, Vector3 pos, Sprite sprite)
    //{
    //    GameObject go = new GameObject(name);
    //    var sr = go.AddComponent<SpriteRenderer>();
    //    sr.sprite = sprite;
    //    sr.material = pixelPerfect;
    //    go.transform.SetParent(target);
    //    go.transform.localPosition = new Vector3(pos.x * 8, pos.y * -8, 0);
    //}
}

