using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class MekaiFactory: BaseEnemyFactory
{
    public MekaiFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public override Enemy Create()
    {
        Sprite bladeSprite = MakeSprite(bladeAsset, bladeSpeed,1);
        Sprite bodySprite = MakeSprite(bodyAsset, bodySpeed);
        Icon ico = new Icon(iconAsset);
        Dictionary<string, Sprite> complexion = new()
        {
            { Enemy.BodyName, bodySprite },
            { Mekai.BladeName, bladeSprite },
        };
        Enemy returner = new EnemyBuilder<Mekai>()
                            .SetAtk(atk)
                            .SetDef(def)
                            .SetFace(ico)
                            .SetName(name)
                            .SetHp(maxHp)
                            .SetBody(complexion)
                            .Build();
        return returner;
    }
    private readonly string bodyAsset = "mekai_body";
    private readonly string bladeAsset = "mekai_blades";
    private readonly float bodySpeed = 0.2f;
    private readonly float bladeSpeed = 0.4f;
    private readonly string name = "Mek-AI Mk.I";
    private readonly string iconAsset = "mekai";
    private readonly int def = 0;
    private readonly int atk = 20;
    private readonly int maxHp = 50;

}