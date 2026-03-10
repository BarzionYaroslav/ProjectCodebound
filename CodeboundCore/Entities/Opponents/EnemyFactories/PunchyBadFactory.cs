using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class PunchyBadFactory: IEnemyFactory
{
    public int X { get { return x; } set { x = value; } }
    public int Y { get { return y; } set { y = value; } }
    public int Depth {
        get { return depth; }
        set
        {
            if (value >= 0)
                depth = value;
        } 
    }

    public PunchyBadFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public Enemy Create()
    {
        Sprite spr = new Sprite(@"./assets/punchy_bad.gif", X, Y, 0.25f, Depth);
        Enemy returner = new PunchyBag("Punchy Bad", 0, 0, 10, 15, spr);
        return returner;
    }
    private int x;
    private int y;
    private int depth;
}