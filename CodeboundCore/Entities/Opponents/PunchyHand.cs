using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class PunchyBadHand : Enemy
{
    public bool Flip {get { return flip; } set { flip = value; }}
    private bool flip;
    public PunchyBadHand(string name, int def, int atk, int hp, int maxHp, Sprite body, bool flip)
    : base(name, def, atk, hp, maxHp, body)
    {
        Game.UpdateStarted += UpdateValues;
        Flip = flip;
    }
    
    public override void UpdateValues()
    {
        double change;
        if (!flip)
        {
            change = Math.Sin((MathF.PI / 180) * Game.Siner * 5) * 4;
            Body.Z = (int)change;
            change = Math.Cos((MathF.PI / 180) * Game.Siner * 5) * 3;
            Body.X = Body.StartX + (int)change;
            change = Math.Sin((MathF.PI / 180) * Game.Siner * 5) * 2;
            Body.Y = Body.StartY + (int)change;
        }
        else
        {
            change = Math.Sin(180+(MathF.PI / 180) * Game.Siner * 5) * 4;
            Body.Z = (int)change;
            change = Math.Cos((MathF.PI / 180) * Game.Siner * 5) * 3;
            Body.X = Body.StartX + (int)change;
            change = Math.Sin((MathF.PI / 180) * Game.Siner * 5) * 2;
            Body.Y = Body.StartY + (int)change;
        }
        
    }
}