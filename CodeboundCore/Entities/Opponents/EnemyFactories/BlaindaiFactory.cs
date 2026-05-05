using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class BlaindaiFactory: BaseEnemyFactory
{
     public BlaindaiFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public override Enemy Create()
    {
        Sprite bodySprite = MakeSprite(bodyAsset, bodySpeed,1);
        Sprite haloSprite = MakeSprite(haloAsset, haloSpeed);
        Dictionary<string, Sprite> complexion = new()
        {
            { Enemy.BodyName, bodySprite },
            { Blaindai.HaloName, haloSprite }
        };
        Icon ico = new Icon(iconAsset);
        Enemy returner = new EnemyBuilder<Blaindai>()
                            .SetAtk(atk)
                            .SetDef(def)
                            .SetFace(ico)
                            .SetName(name)
                            .SetHp(maxHp)
                            .SetBody(complexion)
                            .Build();
        return returner;
    }
    private readonly string bodyAsset = "Blaindai";
    private readonly string haloAsset = "BlaindaiHalo";
    private readonly float bodySpeed = 0.4f;
    private readonly float haloSpeed = 0.4f;
    private readonly string name = "Blaindai";
    private readonly string iconAsset = "blaindai";
    private readonly int def = 5;
    private readonly int atk = 6;
    private readonly int maxHp = 35;

}