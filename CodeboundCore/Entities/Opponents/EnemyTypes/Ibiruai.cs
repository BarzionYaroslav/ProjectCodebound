using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class Ibiruai : Enemy
{
    public Ibiruai(string name, int def, int atk, int hp, int maxHp, Sprite body)
    : base(name, def, atk, hp, maxHp, body)
    {
        GameManager.UpdateStarted += UpdateValues;
    }
    
    public override void UpdateValues()
    {
        var change = Math.Sin((GameManager.Siner + Body.Depth*15) * (MathF.PI / 180) * 2) * 2;
        Body.Y = Body.StartY + (int)change;
    }
}