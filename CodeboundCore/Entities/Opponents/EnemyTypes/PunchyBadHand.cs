using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class PunchyBadHand : Enemy
{
    public bool Flip {get { return flip; } set { flip = value; }}
    private bool flip;

    public PunchyBadHand()
    {
        flip = false;
    }
    public override void UpdateValues()
    {
        double change;
        Sprite bod = Body[BodyName];
        if (!flip)
        {
            change = GameManager.DSin(GameManager.Siner * 5) * 4;
            bod.Z = (int)change;
            change = GameManager.DCos(GameManager.Siner * 5) * 3;
            bod.X = bod.StartX + (int)change;
            change = GameManager.DSin(GameManager.Siner * 5) * 2;
            bod.Y = bod.StartY + (int)change;
        }
        else
        {
            change = GameManager.DSin(180+GameManager.Siner * 5) * 4;
            bod.Z = (int)change;
            change = GameManager.DCos(GameManager.Siner * 5) * 3;
            bod.X = bod.StartX + (int)change;
            change = GameManager.DSin(GameManager.Siner * 5) * 2;
            bod.Y = bod.StartY + (int)change;
        }
        
    }
}