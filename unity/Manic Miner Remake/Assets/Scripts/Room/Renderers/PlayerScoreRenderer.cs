using Com.SloanKelly.ZXSpectrum;

public class PlayerScoreRenderer : IRenderer
{
    const string ScoreFormat = "High Score {0:000000}   Score {1:000000}";

    private GameController _ctrl;
    private SpectrumScreen _screen;

    public PlayerScoreRenderer(GameController ctrl)
    {
        _ctrl = ctrl;
    }

    public void Draw()
    {
        for (int x = 0; x < 32; x++)
            _screen.SetAttribute(x, 19, 6, 0);

        _screen.PrintMessage(0, 19, string.Format(ScoreFormat, _ctrl.HiScore, _ctrl.Score));
    }

    public void Init(SpectrumScreen screen)
    {
        _screen = screen;
    }
}
