using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class IbiruaiFactory: BaseEnemyFactory
{
    public IbiruaiFactory(int x, int y, int depth)
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
        Enemy returner = new EnemyBuilder<Ibiruai>()
                            .SetAtk(atk)
                            .SetDef(def)
                            .SetFace(ico)
                            .SetName(name)
                            .SetHp(maxHp)
                            .SetBody(complexion)
                            .Build();
        return returner;
    }
    private readonly string bodyAsset = "Ibiruai";
    private readonly float bodySpeed = 0.35f;
    private readonly string name = "Ibiruai";
    private readonly string iconAsset = "ibiruai";
    private readonly int def = 0;
    private readonly int atk = 0;
    private readonly int maxHp = 15;
}