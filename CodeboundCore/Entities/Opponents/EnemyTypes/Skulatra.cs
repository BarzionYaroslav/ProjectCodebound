using Codebound.Drawing;
using Codebound.System;
using System;
namespace Codebound.Entities.Opponents;

public class Skulatra : Enemy
{
    public Sprite Head
    {
        get { return head; }
        set
        {
            if (value != null)
                head = value;
            else
                throw new NullReferenceException();
        }
    }

    public Skulatra()
    {
        head = new Sprite();
    }
    public Skulatra(string name, int def, int atk, int hp, int maxHp, Sprite body, Icon face, Sprite head)
    : base(name, def, atk, hp, maxHp, body, face)
    {
        Random rnd = new Random();
        GameManager.UpdateStarted += UpdateValues;
        if (head != null)
            this.head = head;
        else
            this.head = new Sprite();
        shmoves = rnd.Next(2);
    }

    public override void UpdateValues()
    {
        var degree = (360 / Body.ImageCount) * Body.ImageIndex;
        var change = Math.Sin(degree * (MathF.PI / 180)) * 2;
        if (shmoves != 1)
        {
            Head.X = Head.StartX - (int)change;
            if (change >= 1)
                Head.ImageIndex = 3;
            else if (change <= -1)
                Head.ImageIndex = 1;
            else
                Head.ImageIndex = 0;
        }
        else
        {
            Head.X = Head.StartX + (int)change;
            if (change >= 1)
                Head.ImageIndex = 1;
            else if (change <= -1)
                Head.ImageIndex = 3;
            else
                Head.ImageIndex = 0;
        }
        change = Math.Abs(Math.Cos(degree * (MathF.PI / 180)) * 2);
        Head.Y = Head.StartY - (int)change;
    }
    private Sprite head;
    private int shmoves;
}