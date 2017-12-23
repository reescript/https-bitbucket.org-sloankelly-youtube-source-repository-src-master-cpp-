using Com.SloanKelly.ZXSpectrum;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpectrumScreen))]
public class RoomRenderer : MonoBehaviour
{
    private SpectrumScreen screen;

    private IList<IRenderer> renderers;

    public Transform target;

    [Tooltip("The material that has the pixel perfect check box")]
    public Material pixelPerfect;   
    
    void Start()
    {
        screen = GetComponent<SpectrumScreen>();
        var sr = target.GetComponent<SpriteRenderer>();
        sr.sprite = Sprite.Create(screen.Texture, 
                                  new Rect(0, 0, 256, 192), 
                                  new Vector2(0, 1), 1f);
    }

    public void Init(IList<IRenderer> renderers)
    {
        var tmp = new List<IRenderer>();
        foreach(var r in renderers)
        {
            r.Init(screen);
            tmp.Add(r);
        }

        this.renderers = tmp;
    }

    public void DrawScreen()
    {
        screen.Clear(7, 0, false);

        foreach (var r in renderers)
        {
            r.Draw();
        }
    }
}
