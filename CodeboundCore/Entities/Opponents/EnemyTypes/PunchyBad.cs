using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class PunchyBad : Enemy
{
    public override void UpdateValues()
    {
        Sprite bod = Body[BodyName];
        var change = GameManager.DSin(GameManager.Siner * 4) * 3;
        bod.Y = bod.StartY + (int)change;
    }
}