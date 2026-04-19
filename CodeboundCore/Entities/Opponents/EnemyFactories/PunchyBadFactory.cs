using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class PunchyBadFactory: BaseEnemyFactory
{
    public PunchyBadFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public override Enemy Create()
    {
        Sprite bodySprite = MakeSprite(bodyAsset, bodySpeed);
        Icon ico = new Icon(iconAsset, iconSpeed);
        Dictionary<string, Sprite> complexion = new()
        {
            { Enemy.BodyName, bodySprite },
        };
        Enemy returner = new EnemyBuilder<PunchyBad>()
                            .SetAtk(atk)
                            .SetDef(def)
                            .SetFace(ico)
                            .SetName(name)
                            .SetHp(maxHp)
                            .SetBody(complexion)
                            .Build();
        return returner;
    }
    private readonly string bodyAsset = "punchy_bad_new";
    private readonly float bodySpeed = 0.35f;
    private readonly string name = "Punchy Bad";
    private readonly string iconAsset = "badsurprise";
    private readonly float iconSpeed = 0.1f;
    private readonly int def = 0;
    private readonly int atk = 0;
    private readonly int maxHp = 15;
}