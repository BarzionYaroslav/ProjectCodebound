using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class BlaindaiFactory: IEnemyFactory
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

    public BlaindaiFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public Enemy Create()
    {
        Sprite spr = new SpriteBuilder().SetSprite(bodyAsset)
                        .SetPosition(X,Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(bodySpeed)
                        .Build();
        Sprite spr2 = new SpriteBuilder().SetSprite(haloAsset)
                        .SetPosition(X,Y)
                        .SetDepth(Depth)
                        .SetImageSpeed(haloSpeed)
                        .Build(); ;
        Icon ico = new Icon(iconAsset,0f);
        Enemy returner = new Blaindai(name, def, atk, maxHp, maxHp, spr, ico, spr2);
        return returner;
    }
    private int x;
    private int y;
    private int depth;
    private readonly string bodyAsset = "Blaindai";
    private readonly string haloAsset = "BlaindaiHalo";
    private readonly float bodySpeed = 0.4f;
    private readonly float haloSpeed = 0.4f;
    private readonly string name = "Blaindai";
    private readonly string iconAsset = "blaindai";
    private readonly int def = 0;
    private readonly int atk = 0;
    private readonly int maxHp = 15;

}