using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class PunchyBag : Enemy
{
    public override void UpdateValues()
    {
        Sprite bod = body[BodyName];
        var changeY = MathFunctions.DSin(GameManager.Siner * waveSpeed) * waveMagnitude;
        bod.Y = bod.StartY + (int)changeY;
    }
    private readonly int waveSpeed = 2;
    private readonly int waveMagnitude = 2;
}