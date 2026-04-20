using Codebound.Drawing;
using Codebound.System.Randomness;

namespace Codebound.Entities.Opponents;
public class PunchyBagFactory: BaseEnemyFactory
{
    public PunchyBagFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public override Enemy Create()
    {
        Sprite bodySprite = MakeSprite(bodyAsset, bodySpeed);
        Icon ico = new Icon(iconAsset);
        Dictionary<string, Sprite> complexion = new()
            {
                { Enemy.BodyName, bodySprite },
            };
        IRandomList<IEnemyActionStrategy> actions = new RandomList<IEnemyActionStrategy>(
            [
                new EnemySkipStrategy()
            ]
        );
        Enemy returner = new EnemyBuilder<PunchyBag>()
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
    private readonly string bodyAsset = "punchy_bag2";
    private readonly float bodySpeed = 0.25f;
    private readonly string name = "Punchy Bag";
    private readonly string iconAsset = "punchy";
    private readonly int def = 10;
    private readonly int atk = 0;
    private readonly int maxHp = 15;
}