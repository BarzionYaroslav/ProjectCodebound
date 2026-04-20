using Codebound.Drawing;
using Codebound.System.Randomness;
using Codebound.System;

namespace Codebound.Entities.Opponents;
public class SkulatraFactory: BaseEnemyFactory
{
    public SkulatraFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public override Enemy Create()
    {
        string name;
        switch (GameManager.Instance.Randomizer.GetInt(2))
        {
            case 0:
                name = name1;
                break;
            default:
                name = name2;
                break;
        }
        Sprite bodySpr = MakeSprite(bodyAsset, bodySpeed);
        Sprite headSpr = new SpriteBuilder().SetSprite(headAsset)
                        .SetPosition(X + headXOffset, Y + headYOffset)
                        .SetDepth(Depth)
                        .SetImageSpeed(headSpeed)
                        .Build();
        Icon ico = new Icon(iconAsset);
        Dictionary<string, Sprite> complexion = new()
            {
                { Enemy.BodyName, bodySpr },
                { Skulatra.HeadName, headSpr },
            };
        IRandomList<IEnemyActionStrategy> actions = new RandomList<IEnemyActionStrategy>(
            [
                new EnemyPunchStrategy(),
                new EnemySkipStrategy(),
                new EnemyAttackBuffStrategy()
            ]
        );
        Enemy returner = new EnemyBuilder<Skulatra>()
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
    private readonly string bodyAsset = "skulatra_body";
    private readonly float bodySpeed = 0.75f;
    private readonly string headAsset = "skulatra_head";
    private readonly float headSpeed = 0f;
    private readonly int headXOffset = 8;
    private readonly int headYOffset = 6;
    private readonly string name1 = "Mr. Skulatra";
    private readonly string name2 = "Ms. Skulatra";
    private readonly string iconAsset = "skulatra";
    private readonly int def = 6;
    private readonly int atk = 8;
    private readonly int maxHp = 40;
}