using Codebound.Drawing;
namespace Codebound.Entities.Opponents;

public class PunchyBadHandFactory : IEnemyFactory
{
    public int X { get { return x; } set { x = value; } }
    public int Y { get { return y; } set { y = value; } }
    public int Depth
    {
        get { return depth; }
        set
        {
            if (value >= 0)
                depth = value;
        }
    }

    public PunchyBadHandFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public Enemy Create()
    {
        Enemy returner;
        if (X>45)
        {
            Sprite spr = new SpriteBuilder().SetPath(bodyAsset2)
                        .SetPosition(X,Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(bodySpeed)
                        .Build();
            returner = new PunchyBadHand(name, def, atk, maxHp, maxHp, spr, true);
        }
        else
        {
            Sprite spr = new SpriteBuilder().SetPath(bodyAsset1)
                        .SetPosition(X,Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(bodySpeed)
                        .Build();
            returner = new PunchyBadHand(name, def, atk, maxHp, maxHp, spr, false);
        }
        return returner;
    }
    private int x;
    private int y;
    private int depth;
    private readonly string bodyAsset1 = @"./assets/bad_arm1.gif";
    private readonly string bodyAsset2 = @"./assets/bad_arm2.gif";
    private readonly float bodySpeed = 0.25f;
    private readonly string name = "Punchy Hand";
    private readonly int def = 0;
    private readonly int atk = 0;
    private readonly int maxHp = 15;
}