using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class MekaiFactory: IEnemyFactory
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

    public MekaiFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public Enemy Create()
    {
        Sprite spr2 = new SpriteBuilder().SetSprite(bladeAsset)
                        .SetPosition(X,Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(bladeSpeed)
                        .Build();;
        Sprite spr = new SpriteBuilder().SetSprite(bodyAsset)
                        .SetPosition(X,Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(bodySpeed)
                        .Build();
        Enemy returner = new Mekai(name, def, atk, maxHp, maxHp, spr, spr2);
        return returner;
    }
    private int x;
    private int y;
    private int depth;
    private readonly string bodyAsset = "mekai_body";
    private readonly string bladeAsset = "mekai_blades";
    private readonly float bodySpeed = 0.2f;
    private readonly float bladeSpeed = 0.4f;
    private readonly string name = "Mek-AI Mk.I";
    private readonly int def = 0;
    private readonly int atk = 0;
    private readonly int maxHp = 15;

}