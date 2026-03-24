using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class SkulatraFactory: IEnemyFactory
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

    public SkulatraFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public Enemy Create()
    {
        Random rand = new Random();
        string name;
        switch (rand.Next(2))
        {
            case 0:
                name = name1;
                break;
            default:
                name = name2;
                break;
        }
        Sprite bodySpr = new SpriteBuilder().SetSprite(bodyAsset)
                        .SetPosition(X,Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(bodySpeed)
                        .Build();
        Sprite headSpr = new SpriteBuilder().SetSprite(headAsset)
                        .SetPosition(X + headXOffset, Y + headYOffset)
                        .SetDepth(Depth)
                        .SetImageSpeed(headSpeed)
                        .Build();
        Icon ico = new Icon(iconAsset, 0f);
        Enemy returner = new Skulatra(name, def, atk, maxHp, maxHp, bodySpr, ico, headSpr);
        return returner;
    }
    private int x;
    private int y;
    private int depth;
    private readonly string bodyAsset = "skulatra_body";
    private readonly float bodySpeed = 0.75f;
    private readonly string headAsset = "skulatra_head";
    private readonly float headSpeed = 0f;
    private readonly int headXOffset = 8;
    private readonly int headYOffset = 6;
    private readonly string name1 = "Mr. Skulatra";
    private readonly string name2 = "Ms. Skulatra";
    private readonly string iconAsset = "skulatra";
    private readonly int def = 0;
    private readonly int atk = 0;
    private readonly int maxHp = 15;
}