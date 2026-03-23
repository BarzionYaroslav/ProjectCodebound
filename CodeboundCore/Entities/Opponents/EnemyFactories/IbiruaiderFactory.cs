using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class IbiruaiderFactory: IEnemyFactory
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

    public IbiruaiderFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public Enemy Create()
    {
        Sprite sprBody = new SpriteBuilder().SetSprite(bodyAsset)
                        .SetPosition(X, Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(bodySpeed)
                        .Build();
        Sprite sprMid = new SpriteBuilder().SetSprite(midAsset)
                        .SetPosition(X, Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(midSpeed)
                        .Build();
        Sprite sprHead = new SpriteBuilder().SetSprite(headAsset)
                        .SetPosition(X, Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(headSpeed)
                        .Build();
        Enemy returner = new Ibiruaider(name, def, atk, maxHp, maxHp, sprBody, sprMid, sprHead);
        return returner;
    }
    private int x;
    private int y;
    private int depth;
    private readonly string bodyAsset = "ibiruaider_main";
    private readonly float bodySpeed = 0.35f;
    private readonly string midAsset = "ibiruaider_mid";
    private readonly float midSpeed = 0.35f;
    private readonly string headAsset = "ibiruaider_head";
    private readonly float headSpeed = 0f;
    private readonly string name = "Ibiruaider (I&Y)";
    private readonly int def = 0;
    private readonly int atk = 0;
    private readonly int maxHp = 15;
}