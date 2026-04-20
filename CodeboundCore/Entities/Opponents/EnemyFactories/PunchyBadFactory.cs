using Codebound.Drawing;
using Codebound.System.Randomness;

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
        IRandomList<IEnemyActionStrategy> actions = new RandomList<IEnemyActionStrategy>(
            [
                new EnemyPunchStrategy(),
                new EnemySkipStrategy()
            ]
        );
        Enemy returner = new EnemyBuilder<PunchyBad>()
                            .SetAtk(atk)
                            .SetDef(def)
                            .SetFace(ico)
                            .SetName(name)
                            .SetHp(maxHp)
                            .SetBody(complexion)
                            .SetActionList(actions)
                            .Build();
        return returner;
    }
    private readonly string bodyAsset = "punchy_bad_new";
    private readonly float bodySpeed = 0.4f;
    private readonly string name = "Punchy Bad";
    private readonly string iconAsset = "badsurprise";
    private readonly float iconSpeed = 0.1f;
    private readonly int def = 20;
    private readonly int atk = 5;
    private readonly int maxHp = 70;
}