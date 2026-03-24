using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class Yokanten : Enemy
{
    public Sprite Tail
    {
        get { return tail; }
        set
        {
            if (value != null)
                tail = value;
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

    public Yokanten()
    {
        this.middle = new Sprite();
        this.tail = new Sprite();
    }

    public Yokanten(string name, int def, int atk, int hp, int maxHp, Sprite body, Icon face, Sprite middle, Sprite tail)
    : base(name, def, atk, hp, maxHp, body, face)
    {
        GameManager.UpdateStarted += UpdateValues;
        if (middle != null)
            this.middle = middle;
        else
            this.middle = new Sprite();
        if (tail != null)
            this.tail = tail;
        else
            this.tail = new Sprite();
        for (int i = 0; i < posCount; i++)
        {
            positions.Add((Body.X, Body.Y, Body.ImageIndex));
        }
    }
    public override void UpdateValues()
    {
        int bodyChange = (int)(Math.Sin(GameManager.Siner * (MathF.PI / 180) * 3) * 8);
        int turnChange = (int)(Math.Cos(GameManager.Siner * (MathF.PI / 180) * 3) * 8);
        if (turnChange > 2)
            Body.ImageIndex = 1;
        else if (turnChange < -2)
            Body.ImageIndex = 2;
        else
            Body.ImageIndex = 0;
        Body.X = Body.StartX + bodyChange;
        positions.Insert(0, (Body.X, Body.Y, Body.ImageIndex));
        positions.RemoveAt(posCount);
        Middle.X = positions[midIndex].X;
        Middle.Y = positions[midIndex].Y;
        Middle.ImageIndex = positions[midIndex].Frame;
        Tail.X = positions[tailIndex].X;
        Tail.Y = positions[tailIndex].Y;
        Tail.ImageIndex = positions[tailIndex].Frame;
    }

    private List<(int X, int Y, float Frame)> positions = new List<(int X, int Y, float Frame)>();
    private Sprite middle;
    private Sprite tail;
    private readonly int posCount = 120;
    private readonly int midIndex = 35;
    private readonly int tailIndex = 60;
}