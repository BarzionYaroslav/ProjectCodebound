using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class Mekai : Enemy
{
    public Sprite Blades
    {
        get { return blades; }
        set
        {
            if (value != null)
                blades = value;
            else
                throw new NullReferenceException();
        }
    }
    public Mekai(string name, int def, int atk, int hp, int maxHp, Sprite body, Sprite blades)
    : base(name, def, atk, hp, maxHp, body)
    {
        GameManager.UpdateStarted += UpdateValues;
        if (blades != null)
            this.blades = blades;
        else
            this.blades = new Sprite();
    }

    public override void UpdateValues()
    {
        var sinner = (GameManager.Siner + Body.Depth * depthOffset) * (MathF.PI / 180);
        var change = Math.Sin(sinner * waveSpeed) * waveYMagnitude;
        Body.Y = Body.StartY + Math.Clamp((int)change, -maxChange, maxChange);
        Blades.X = Body.X;
        Blades.Y = Body.Y;
    }
    private Sprite blades;
    private readonly int depthOffset = 15;
    private readonly int waveYMagnitude = 12;
    private readonly int waveSpeed = 5;
    private readonly int maxChange = 3;
}