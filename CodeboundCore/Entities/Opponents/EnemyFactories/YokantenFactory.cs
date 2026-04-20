using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class YokantenFactory: BaseEnemyFactory
{
    public YokantenFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public override Enemy Create()
    {
        Sprite tailSprite = MakeSprite(tailAsset, tailSpeed);
        Sprite middleSprite = MakeSprite(midAsset, midSpeed);
        Sprite headSprite = MakeSprite(headAsset, headSpeed);
        Icon ico = new Icon(iconAsset);
        Dictionary<string, Sprite> complexion = new()
            {
                { Enemy.BodyName, headSprite },
                { Yokanten.MidName, middleSprite },
                { Yokanten.TailName, tailSprite },
            };
        Enemy returner = new EnemyBuilder<Yokanten>()
                            .SetAtk(atk)
                            .SetDef(def)
                            .SetFace(ico)
                            .SetName(name)
                            .SetHp(maxHp)
                            .SetBody(complexion)
                            .Build();
        return returner;
    }
    private readonly string headAsset = "yokanten_head";
    private readonly float headSpeed = 0f;
    private readonly string midAsset = "yokanten_body";
    private readonly float midSpeed = 0f;
    private readonly string tailAsset = "yokanten_tail";
    private readonly float tailSpeed = 0f;
    private readonly string name = "Yokanten";
    private readonly string iconAsset = "yokanten";
    private readonly int def = 0;
    private readonly int atk = 0;
    private readonly int maxHp = 15;
}