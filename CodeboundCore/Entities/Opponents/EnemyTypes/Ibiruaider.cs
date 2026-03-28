using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class Ibiruaider : Enemy
{

    public Ibiruaider() : base()
    {
        Expectations.Add(MidName);
        Expectations.Add(HeadName);
        Body.ChangeExpectations(Expectations);
    }

    public override void AfterPrep()
    {
        base.AfterPrep();
        for (int i = 0; i < posCount; i++)
        {
            positions.Add((Body[BodyName].X, Body[BodyName].Y, Body[BodyName].ImageIndex));
        }
    }
    public override void UpdateValues()
    {
        Sprite bod = Body[BodyName];
        Sprite mid = Body[MidName];
        Sprite head = Body[HeadName];
        int bodyChangeX = (int)(GameManager.DSin(GameManager.Siner * 3) * 8);
        int bodyChangeY = (int)(GameManager.DSin((GameManager.Siner + bod.Depth*15) * 3) * 2);
        int turnChange = (int)(GameManager.DCos(GameManager.Siner * 3) * 8);
        int headFrameOffset;
        if (turnChange > 1)
            headFrameOffset = 2;
        else if (turnChange < -1)
            headFrameOffset = 1;
        else
            headFrameOffset = 0;
        bod.X = bod.StartX + bodyChangeX;
        bod.Y = bod.StartY + bodyChangeY;
        positions.Insert(0, (bod.X, bod.Y, bod.ImageIndex));
        positions.RemoveAt(posCount);
        mid.X = positions[midIndex].X;
        mid.Y = positions[midIndex].Y + 7;
        mid.ImageIndex = bod.ImageIndex;
        head.X = positions[headIndex].X;
        head.Y = positions[headIndex].Y + 14;
        head.ImageIndex = bod.ImageIndex + headFrameOffset*bod.ImageCount;
    }

    private List<(int X, int Y, float Frame)> positions = new List<(int X, int Y, float Frame)>();
    private readonly int posCount = 120;
    private readonly int midIndex = 10;
    private readonly int headIndex = 20;
    static public readonly string MidName = "mid";
    static public readonly string HeadName = "head";
}