using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public abstract class BaseEnemyFactory : IEnemyFactory
{
    public int X { get { return x; } set { x = value; } }
    public int Y { get { return y; } set { y = value; } }
    public int Depth
    {
        get { return depth; }
        set
        {
            if (value >= 0)
                depth = value;
        }
    }

    public abstract Enemy Create();
    public virtual Sprite MakeSprite(string asset, float imageSpeed)
    {
        Sprite spr = new SpriteBuilder().SetSprite(asset)
                .SetPosition(X, Y)
                .SetDepth(Depth)
                .SetImageSpeed(imageSpeed)
                .Build();
        return spr;
    }
    
    private int x;
    private int y;
    private int depth;
}