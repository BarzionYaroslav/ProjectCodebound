using Codebound.Drawing;
using Codebound.System;
using Codebound.System.Functions;
namespace Codebound.Entities.Opponents;

public class Mekai : Enemy
{
    public Mekai() : base()
    {
        Expectations.Add(BladeName);
        body.ChangeExpectations(Expectations);
    }

    public override void UpdateValues()
    {
        Sprite bod = body[BodyName];
        Sprite blades = body[BladeName];
        var sinner = GameManager.Siner + bod.Depth * depthOffset;
        var changeY = MathFunctions.DSin(sinner * waveSpeed) * waveYMagnitude;
        bod.Y = bod.StartY + Math.Clamp((int)changeY, -maxChange, maxChange);
        blades.X = bod.X;
        blades.Y = bod.Y;
    }
    private readonly int depthOffset = 15;
    private readonly int waveYMagnitude = 12;
    private readonly int waveSpeed = 5;
    private readonly int maxChange = 3;
    static public readonly string BladeName = "blades";
}