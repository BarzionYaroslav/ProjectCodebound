using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class Ibiruaider : Enemy
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
    public Sprite Middle
    {
        get { return middle; }
        set
        {
            if (value != null)
                middle = value;
            else
                throw new NullReferenceException();
        }
    }

    public Ibiruaider()
    {
        this.middle = new Sprite();
        this.head = new Sprite();
    }

    public Ibiruaider(string name, int def, int atk, int hp, int maxHp, Sprite body, Icon face, Sprite middle, Sprite head)
    : base(name, def, atk, hp, maxHp, body,face)
    {
        GameManager.UpdateStarted += UpdateValues;
        if (middle != null)
            this.middle = middle;
        else
            this.middle = new Sprite();
        if (head != null)
            this.head = head;
        else
            this.head = new Sprite();
        for (int i = 0; i < posCount; i++)
        {
            positions.Add((Body.X, Body.Y, Body.ImageIndex));
        }
    }
    public override void UpdateValues()
    {
        int bodyChangeX = (int)(Math.Sin(GameManager.Siner * (MathF.PI / 180) * 3) * 8);
        int bodyChangeY = (int)(Math.Sin((GameManager.Siner + Body.Depth*15) * (MathF.PI / 180) * 3) * 2);
        int turnChange = (int)(Math.Cos(GameManager.Siner * (MathF.PI / 180) * 3) * 8);
        int headFrameOffset;
        if (turnChange > 1)
            headFrameOffset = 2;
        else if (turnChange < -1)
            headFrameOffset = 1;
        else
            headFrameOffset = 0;
        Body.X = Body.StartX + bodyChangeX;
        Body.Y = Body.StartY + bodyChangeY;
        positions.Insert(0, (Body.X, Body.Y, Body.ImageIndex));
        positions.RemoveAt(posCount);
        Middle.X = positions[midIndex].X;
        Middle.Y = positions[midIndex].Y + 7;
        Middle.ImageIndex = Body.ImageIndex;
        Head.X = positions[headIndex].X;
        Head.Y = positions[headIndex].Y + 14;
        Head.ImageIndex = Body.ImageIndex + headFrameOffset*Body.ImageCount;
    }

    private List<(int X, int Y, float Frame)> positions = new List<(int X, int Y, float Frame)>();
    private Sprite middle;
    private Sprite head;
    private readonly int posCount = 120;
    private readonly int midIndex = 20;
    private readonly int headIndex = 40;
}