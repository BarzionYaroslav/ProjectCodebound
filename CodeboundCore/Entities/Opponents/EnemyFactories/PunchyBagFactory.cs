using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class PunchyBagFactory: IEnemyFactory
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

    public PunchyBagFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public Enemy Create()
    {
        Sprite spr = new SpriteBuilder().SetPath(bodyAsset)
                        .SetPosition(X,Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(bodySpeed)
                        .Build();
        Enemy returner = new PunchyBag(name, def, atk, maxHp, maxHp, spr);
        return returner;
    }
    private int x;
    private int y;
    private int depth;
    private readonly string bodyAsset = @"./assets/punchy_bag.gif";
    private readonly float bodySpeed = 0.25f;
    private readonly string name = "Punchy Bag";
    private readonly int def = 0;
    private readonly int atk = 0;
    private readonly int maxHp = 15;
}