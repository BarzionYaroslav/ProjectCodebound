using Codebound.Drawing;
namespace Codebound.Entities.Opponents;

public class PunchyBadHandFactory : IEnemyFactory
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

    public PunchyBadHandFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public Enemy Create()
    {
        Enemy returner;
        if (X>45)
        {
            Sprite spr = new Sprite(@"./assets/bad_arm2.gif", X, Y, 0.25f, Depth);
            returner = new PunchyBadHand("Punchy Hand", 0, 0, 10, 15, spr, true);
        }
        else
        {
            Sprite spr = new Sprite(@"./assets/bad_arm1.gif", X, Y, 0.25f, Depth);
            returner = new PunchyBadHand("Punchy Hand", 0, 0, 10, 15, spr, false);
        }
        return returner;
    }
    private int x;
    private int y;
    private int depth;
}