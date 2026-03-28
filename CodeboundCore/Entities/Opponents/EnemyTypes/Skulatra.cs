using Codebound.Drawing;
using Codebound.System;
using System;
namespace Codebound.Entities.Opponents;

public class Skulatra : Enemy
{
    public Skulatra() : base()
    {
        Expectations.Add(HeadName);
        Body.ChangeExpectations(Expectations);
        Random rnd = new Random();
        shmoves = rnd.Next(2);
    }

    public override void UpdateValues()
    {
        Sprite bod = Body[BodyName];
        Sprite head = Body[HeadName];
        var degree = (360 / bod.ImageCount) * bod.ImageIndex;
        var change = GameManager.DSin(degree) * 2;
        if (shmoves != 1)
        {
            head.X = head.StartX - (int)change;
            if (change >= 1)
                head.ImageIndex = 3;
            else if (change <= -1)
                head.ImageIndex = 1;
            else
                head.ImageIndex = 0;
        }
        else
        {
            head.X = head.StartX + (int)change;
            if (change >= 1)
                head.ImageIndex = 1;
            else if (change <= -1)
                head.ImageIndex = 3;
            else
                head.ImageIndex = 0;
        }
        change = Math.Abs(GameManager.DCos(degree) * 2);
        head.Y = head.StartY - (int)change;
    }
    private int shmoves;
    static public readonly string HeadName = "head";
}