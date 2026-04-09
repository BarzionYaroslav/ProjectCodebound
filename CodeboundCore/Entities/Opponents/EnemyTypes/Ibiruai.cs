using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class Ibiruai : Enemy
{
    public override void UpdateValues()
    {
        Sprite bod = body[BodyName];
        var changeY = GameManager.DSin((GameManager.Siner + bod.Depth * depthOffset) * waveSpeed) * waveMagnitude;
        bod.Y = bod.StartY + (int)changeY;
    }
    
    private readonly int depthOffset = 15;
    private readonly int waveMagnitude = 6;
    private readonly int waveSpeed = 2;
}