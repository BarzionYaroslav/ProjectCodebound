using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class Ibiruaider : Enemy
{

    public Ibiruaider() : base()
    {
        Expectations.Add(MidName);
        Expectations.Add(HeadName);
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
        Sprite head = body[HeadName];
        int bodyChangeX = (int)(GameManager.DSin(GameManager.Siner * waveXSpeed) * waveXMagnitude);
        int bodyChangeY = (int)(GameManager.DSin((GameManager.Siner + bod.Depth*depthOffset) * waveYSpeed) * waveYMagnitude);
        int turnChange = (int)(GameManager.DCos(GameManager.Siner * waveXSpeed) * waveXMagnitude);
        int headFrameOffset;
        if (turnChange > headTreshold)
            headFrameOffset = rightFrameOffset;
        else if (turnChange < -headTreshold)
            headFrameOffset = leftFrameOffset;
        else
            headFrameOffset = defaultFrameOffset;
        bod.X = bod.StartX + bodyChangeX;
        bod.Y = bod.StartY + bodyChangeY;
        positions.Insert(0, (bod.X, bod.Y, bod.ImageIndex));
        positions.RemoveAt(posCount);
        mid.X = positions[midIndex].X;
        mid.Y = positions[midIndex].Y + midOffset;
        mid.ImageIndex = bod.ImageIndex;
        head.X = positions[headIndex].X;
        head.Y = positions[headIndex].Y + headOffset;
        head.ImageIndex = bod.ImageIndex + headFrameOffset*bod.ImageCount;
    }

    private List<(int X, int Y, float Frame)> positions = new List<(int X, int Y, float Frame)>();
    private readonly int depthOffset = 15;
    private readonly int waveXMagnitude = 8;
    private readonly int waveYMagnitude = 2;
    private readonly int waveXSpeed = 3;
    private readonly int waveYSpeed = 3;
    private readonly int headTreshold = 1;
    private readonly int midOffset = 7;
    private readonly int headOffset = 14;
    private readonly int defaultFrameOffset = 0;
    private readonly int leftFrameOffset = 1;
    private readonly int rightFrameOffset = 2;
    private readonly int posCount = 120;
    private readonly int midIndex = 10;
    private readonly int headIndex = 20;
    static public readonly string MidName = "mid";
    static public readonly string HeadName = "head";
}