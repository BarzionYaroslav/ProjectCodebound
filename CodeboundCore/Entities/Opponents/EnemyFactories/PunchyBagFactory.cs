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
        Sprite spr = new SpriteBuilder().SetSprite(bodyAsset)
                        .SetPosition(X, Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(bodySpeed)
                        .Build();
        Icon ico = new Icon(iconAsset, 0f);
        Dictionary<string, Sprite> complexion = new()
            {
                { Enemy.BodyName, spr },
            };
        Enemy returner = new EnemyBuilder<PunchyBag>()
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
    private readonly string bodyAsset = "punchy_bag2";
    private readonly float bodySpeed = 0.25f;
    private readonly string name = "Punchy Bag";
    private readonly string iconAsset = "punchy";
    private readonly int def = 0;
    private readonly int atk = 0;
    private readonly int maxHp = 15;
}