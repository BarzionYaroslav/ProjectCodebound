using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class PunchyBad : Enemy
{
    public override void UpdateValues()
    {
        Sprite bod = body[BodyName];
        var changeY = GameManager.DSin(GameManager.Siner * waveSpeed) * waveMagnitude;
        bod.Y = bod.StartY + (int)changeY;
    }

    private readonly int waveSpeed = 4;
    private readonly int waveMagnitude = 3;
}