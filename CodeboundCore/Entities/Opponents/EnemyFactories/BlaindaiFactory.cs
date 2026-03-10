using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class BlaindaiFactory: IEnemyFactory
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

    public BlaindaiFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public Enemy Create()
    {
        Sprite spr = new Sprite(@"./assets/Ibiruai2.gif", X, Y, 0.25f, Depth);
        Sprite spr2 = new Sprite(@"./assets/BlaindaiHalo.gif", X, Y, 0.2f, Depth);
        Enemy returner = new Blaindai("Blaindai", 0, 0, 10, 15, spr, spr2);
        return returner;
    }
    private int x;
    private int y;
    private int depth;
}