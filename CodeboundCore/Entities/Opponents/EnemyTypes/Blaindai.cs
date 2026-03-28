using System.Runtime;
using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class Blaindai : Enemy
{
    public Blaindai() : base()
    {
        Expectations.Add(HaloName);
        Body.ChangeExpectations(Expectations);
    }

    public override void AfterPrep()
    {
        base.AfterPrep();
        prevX = Body[BodyName].X;
        prevY = Body[BodyName].Y;
    }

    public override void UpdateValues()
    {
        Sprite bod = Body[BodyName];
        Sprite halo = Body[HaloName];
        halo.X = prevX;
        halo.Y = prevY;
        prevX = bod.X;
        prevY = bod.Y;
        var sinner = GameManager.Siner + bod.Depth * depthOffset;
        var change = GameManager.DSin(sinner * waveSpeed) * waveYMagnitude;
        bod.Y = bod.StartY + (int)change;
        change = GameManager.DSin(sinner * waveSpeed * waveXMult) * waveXMagnitude;
        bod.X = bod.StartX + (int)change;
    }
    private int prevX;
    private int prevY;
    private readonly int depthOffset = 15;
    private readonly int waveYMagnitude = 6;
    private readonly int waveXMagnitude = 8;
    private readonly int waveSpeed = 2;
    private readonly int waveXMult = 3;
    static public readonly string HaloName = "halo";
}