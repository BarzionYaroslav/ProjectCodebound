using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class PunchyBad : Enemy
{
    public PunchyBad(string name, int def, int atk, int hp, int maxHp, Sprite body)
    : base(name, def, atk, hp, maxHp, body)
    {
        GameManager.UpdateStarted += UpdateValues;
    }
    
    public override void UpdateValues()
    {
        var change = Math.Sin(GameManager.Siner * (MathF.PI / 180) * 4) * 3;
        Body.Y = Body.StartY + (int)change;
    }
}