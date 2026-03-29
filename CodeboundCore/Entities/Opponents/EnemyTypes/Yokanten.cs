using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class Yokanten : Enemy
{
    public Yokanten()
    {
        Expectations.Add(TailName);
        Expectations.Add(MidName);
        body.ChangeExpectations(Expectations);
    }
    public override void AfterPrep()
    {
        base.AfterPrep();
        for (int i = 0; i < posCount; i++)
        {
            positions.Add((body[BodyName].X, body[BodyName].Y, body[BodyName].ImageIndex));
        }
    }

    public override void UpdateValues()
    {
        Sprite bod = body[BodyName];
        Sprite mid = body[MidName];
        Sprite tail = body[TailName];
        int bodyChange = (int)(GameManager.DSin(GameManager.Siner * 3) * 8);
        int turnChange = (int)(GameManager.DCos(GameManager.Siner * 3) * 8);
        if (turnChange > 2)
            bod.ImageIndex = 1;
        else if (turnChange < -2)
            bod.ImageIndex = 2;
        else
            bod.ImageIndex = 0;
        bod.X = bod.StartX + bodyChange;
        positions.Insert(0, (bod.X, bod.Y, bod.ImageIndex));
        positions.RemoveAt(posCount);
        mid.X = positions[midIndex].X;
        mid.Y = positions[midIndex].Y;
        mid.ImageIndex = positions[midIndex].Frame;
        tail.X = positions[tailIndex].X;
        tail.Y = positions[tailIndex].Y;
        tail.ImageIndex = positions[tailIndex].Frame;
    }

    private List<(int X, int Y, float Frame)> positions = new List<(int X, int Y, float Frame)>();
    private readonly int posCount = 120;
    private readonly int midIndex = 15;
    private readonly int tailIndex = 25;
    static public readonly string MidName = "mid";
    static public readonly string TailName = "tail";
}