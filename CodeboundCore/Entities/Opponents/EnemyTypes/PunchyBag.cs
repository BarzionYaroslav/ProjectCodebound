using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class PunchyBag : Enemy
{
    public override void UpdateValues()
    {
        Sprite bod = body[BodyName];
        var change = GameManager.DSin(GameManager.Siner * 2) * 2;
        bod.Y = bod.StartY + (int)change;
    }
}