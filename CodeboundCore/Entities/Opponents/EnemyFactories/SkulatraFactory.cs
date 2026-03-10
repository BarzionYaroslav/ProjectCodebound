using Codebound.Drawing;

namespace Codebound.Entities.Opponents;
public class SkulatraFactory: IEnemyFactory
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

    public SkulatraFactory(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
    }
    public Enemy Create()
    {
        Random rand = new Random();
        string name;
        switch (rand.Next(2))
        {
            case 0:
                name = "Ms. Skulatra";
                break;
            default:
                name = "Mr. Skulatra";
                break;
        }
        Sprite bodySpr = new Sprite(@"./assets/skulatra_body.gif", X, Y, 0.75f, Depth);
        Sprite headSpr = new Sprite(@"./assets/skulatra_head.gif", X + 8, Y + 6, 0, Depth);
        Enemy returner = new Skulatra(name, 0, 0, 10, 15, bodySpr, headSpr);
        return returner;
    }
    private int x;
    private int y;
    private int depth;
}