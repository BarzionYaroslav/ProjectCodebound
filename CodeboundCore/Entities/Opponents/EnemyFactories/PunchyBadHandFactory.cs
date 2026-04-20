using Codebound.Drawing;
using Codebound.System.Randomness;
namespace Codebound.Entities.Opponents;

public class PunchyBadHandFactory : BaseEnemyFactory
{
    public PunchyBadHandFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public override Enemy Create()
    {
        PunchyBadHand returner;
        Icon ico = new Icon(iconAsset);
        bool tresholdReached = X > flipTreshold;
        string usedAsset = tresholdReached? bodyAsset2 : bodyAsset1;
        Sprite bodySprite = MakeSprite(usedAsset, bodySpeed);
        Dictionary<string, Sprite> complexion = new()
        {
            { Enemy.BodyName, bodySprite },
        };
        IRandomList<IEnemyActionStrategy> actions = new RandomList<IEnemyActionStrategy>(
            [
                new EnemyPunchStrategy(),
                new EnemySkipStrategy(),
                new EnemyStrongPunchStrategy()
            ]
        );
        returner = new EnemyBuilder<PunchyBadHand>()
            .SetAtk(atk)
            .SetDef(def)
            .SetFace(ico)
            .SetName(name)
            .SetHp(maxHp)
            .SetBody(complexion)
            .SetActionList(actions)
            .Build();
        returner.Flip = tresholdReached;
        return returner;
    }
    private readonly string bodyAsset1 = "bad_arm1";
    private readonly string bodyAsset2 = "bad_arm2";
    private readonly float bodySpeed = 0.25f;
    private readonly string name = "Handbad";
    private readonly string iconAsset = "handbad";
    private readonly int flipTreshold = 45;
    private readonly int def = 6;
    private readonly int atk = 10;
    private readonly int maxHp = 50;
}