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
        Icon ico = new Icon(iconAsset, 0f);
        Dictionary<string, Sprite> complexion = new()
        {
            { Enemy.BodyName, sprBody },
            { Ibiruaider.MidName, sprMid },
            { Ibiruaider.HeadName, sprHead }
        };
        Enemy returner = new EnemyBuilder<Ibiruaider>()
                            .SetAtk(atk)
                            .SetDef(def)
                            .SetFace(ico)
                            .SetName(name)
                            .SetHp(maxHp)
                            .SetBody(complexion)
                            .Build();
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
    private readonly string iconAsset = "ibiruaider";
    private readonly int def = 0;
    private readonly int atk = 0;
    private readonly int maxHp = 15;
}