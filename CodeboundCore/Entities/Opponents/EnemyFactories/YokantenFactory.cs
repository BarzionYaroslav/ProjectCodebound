using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class YokantenFactory: IEnemyFactory
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

    public YokantenFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public Enemy Create()
    {
        Sprite sprTail = new SpriteBuilder().SetSprite(tailAsset)
                        .SetPosition(X, Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(tailSpeed)
                        .Build();
        Sprite sprMid = new SpriteBuilder().SetSprite(midAsset)
                        .SetPosition(X, Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(midSpeed)
                        .Build();
        Sprite sprHead = new SpriteBuilder().SetSprite(bodyAsset)
                        .SetPosition(X, Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(bodySpeed)
                        .Build();
        Icon ico = new Icon(iconAsset, 0f);
        Dictionary<string, Sprite> complexion = new()
            {
                { Enemy.BodyName, sprHead },
                { Yokanten.MidName, sprMid },
                { Yokanten.TailName, sprTail },
            };
        Enemy returner = new EnemyBuilder<Yokanten>()
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
    private readonly string bodyAsset = "yokanten_head";
    private readonly float bodySpeed = 0f;
    private readonly string midAsset = "yokanten_body";
    private readonly float midSpeed = 0f;
    private readonly string tailAsset = "yokanten_tail";
    private readonly float tailSpeed = 0f;
    private readonly string name = "Yokanten";
    private readonly string iconAsset = "yokanten";
    private readonly int def = 0;
    private readonly int atk = 0;
    private readonly int maxHp = 15;
}