using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class PunchyBad : Enemy
{
    public override void UpdateValues()
    {
        Sprite bod = body[BodyName];
        var changeY = MathFunctions.DSin(GameManager.Siner * waveSpeed) * waveMagnitude;
        var changeX = MathFunctions.DSin((bod.ImageIndex / bod.ImageCount) * 360) * waveMagnitude;
        bod.Y = bod.StartY + (int)changeY;
        bod.X = bod.StartX + (int)changeX;
    }

    private readonly int waveSpeed = 4;
    private readonly int waveMagnitude = 3;
}