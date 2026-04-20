using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class IbiruaiderFactory: BaseEnemyFactory
{
    public IbiruaiderFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public override Enemy Create()
    {
        Sprite bodySprite = MakeSprite(bodyAsset, bodySpeed);
        Sprite middleSprite = MakeSprite(midAsset, midSpeed);
        Sprite headSprite = MakeSprite(headAsset, headSpeed);
        Icon ico = new Icon(iconAsset);
        Dictionary<string, Sprite> complexion = new()
        {
            { Enemy.BodyName, bodySprite },
            { Ibiruaider.MidName, middleSprite },
            { Ibiruaider.HeadName, headSprite }
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
    private readonly string bodyAsset = "ibiruaider_main";
    private readonly float bodySpeed = 0.35f;
    private readonly string midAsset = "ibiruaider_mid";
    private readonly float midSpeed = 0.35f;
    private readonly string headAsset = "ibiruaider_head";
    private readonly float headSpeed = 0f;
    private readonly string name = "Ibiruaider (I&Y)";
    private readonly string iconAsset = "ibiruaider";
    private readonly int def = 6;
    private readonly int atk = 5;
    private readonly int maxHp = 25;
}