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
        // int z = (int)(Math.Sin((MathF.PI / 180) * imageFrame * 5) * 4);
        // stage.Composite(arm1[0], (int)z - 3 + (int)(Math.Cos((MathF.PI / 180) * imageFrame * 5) * 3), -3 + (int)(Math.Sin((MathF.PI / 180) * imageFrame * 5) * 2), CompositeOperator.Over);
        // z = (int)(Math.Sin(180+(MathF.PI / 180) * imageFrame * 5) * 4);
        // stage.Composite(arm2[0], (int)z+(90 - 28) - (int)(Math.Cos((MathF.PI / 180) * imageFrame * 5) * 3), -3 + (int)(Math.Sin((MathF.PI / 180) * imageFrame * 5) * 2), CompositeOperator.Over);
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