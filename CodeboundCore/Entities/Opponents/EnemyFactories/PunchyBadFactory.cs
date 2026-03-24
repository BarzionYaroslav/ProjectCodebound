using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class PunchyBadFactory: IEnemyFactory
{
    public int X { get { return x; } set { x = value; } }
    public int Y { get { return y; } set { y = value; } }
    public int Depth {
        get { return depth; }
        set
        {
            if (value >= 0)
                depth = value;
        } 
    }

    public PunchyBadFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public Enemy Create()
    {
        Sprite spr = new SpriteBuilder().SetSprite(bodyAsset)
                        .SetPosition(X, Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(bodySpeed)
                        .Build();
        Icon ico = new Icon(iconAsset, iconSpeed);
        Enemy returner = new PunchyBad(name, def, atk, maxHp, maxHp, spr, ico);
        return returner;
    }
    private int x;
    private int y;
    private int depth;
    private readonly string bodyAsset = "punchy_bad";
    private readonly float bodySpeed = 0.25f;
    private readonly string name = "Punchy Bad";
    private readonly string iconAsset = "badsurprise";
    private readonly float iconSpeed = 0.1f;
    private readonly int def = 0;
    private readonly int atk = 0;
    private readonly int maxHp = 15;
}