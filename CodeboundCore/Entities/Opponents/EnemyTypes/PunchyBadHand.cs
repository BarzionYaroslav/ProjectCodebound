using Codebound.Drawing;
using Codebound.System;
using Codebound.System.Functions;
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
        Sprite bod = body[BodyName];
        double changeX = MathFunctions.DCos(GameManager.Siner * waveSpeed) * waveXMagnitude;
        double changeY = MathFunctions.DSin(GameManager.Siner * waveSpeed) * waveYMagnitude;
        double changeZ = MathFunctions.DSin(GameManager.Siner * waveSpeed) * waveZMagnitude;
        if (flip)
        {
            changeZ = MathFunctions.DSin(flipOffset + GameManager.Siner * waveSpeed) * waveZMagnitude;
        }
        bod.Z = (int)changeZ;
        bod.X = bod.StartX + (int)changeX;
        bod.Y = bod.StartY + (int)changeY;
    }

    private readonly int flipOffset = 180;
    private readonly int waveSpeed = 5;
    private readonly int waveZMagnitude = 4;
    private readonly int waveXMagnitude = 3;
    private readonly int waveYMagnitude = 2;
}