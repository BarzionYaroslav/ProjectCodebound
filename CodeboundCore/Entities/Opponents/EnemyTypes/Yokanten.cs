using Codebound.Drawing;
using Codebound.System;
using Codebound.System.Functions;
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
        int bodyChangeX = (int)(MathFunctions.DSin(GameManager.Siner * waveSpeed) * waveMagnitude);
        int turnChange = (int)(MathFunctions.DCos(GameManager.Siner * waveSpeed) * waveMagnitude);
        if (turnChange > turnTreshold)
            bod.ImageIndex = rightImageIndex;
        else if (turnChange < -turnTreshold)
            bod.ImageIndex = leftImageIndex;
        else
            bod.ImageIndex = defaultImageIndex;
        bod.X = bod.StartX + bodyChangeX;
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
    private readonly int waveSpeed = 3;
    private readonly int waveMagnitude = 8;
    private readonly int turnTreshold = 2;
    private readonly int defaultImageIndex = 0;
    private readonly int leftImageIndex = 2;
    private readonly int rightImageIndex = 1;
    private readonly int posCount = 120;
    private readonly int midIndex = 15;
    private readonly int tailIndex = 25;
    static public readonly string MidName = "mid";
    static public readonly string TailName = "tail";
}