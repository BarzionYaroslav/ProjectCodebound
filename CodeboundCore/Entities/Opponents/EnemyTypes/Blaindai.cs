using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class Blaindai : Enemy
{
    public Sprite Halo
    {
        get { return halo; }
        set
        {
            if (value != null)
                halo = value;
            else
                throw new NullReferenceException();
        }
    }
    public Blaindai(string name, int def, int atk, int hp, int maxHp, Sprite body, Sprite halo)
    : base(name, def, atk, hp, maxHp, body)
    {
        GameManager.UpdateStarted += UpdateValues;
        if (halo != null)
            this.halo = halo;
        else
            this.halo = new Sprite();
        prevX = Body.X;
        prevY = Body.Y;
    }

    public override void UpdateValues()
    {
        Halo.X = prevX;
        Halo.Y = prevY;
        prevX = Body.X;
        prevY = Body.Y;
        var change = Math.Sin((GameManager.Siner + Body.Depth * 15) * (MathF.PI / 180) * 2) * 6;
        Body.Y = Body.StartY + (int)change;
        change = Math.Cos((GameManager.Siner + Body.Depth * 15) * (MathF.PI / 180) * 6) * 8;
        Body.X = Body.StartX + (int)change;
    }

    private Sprite halo;
    private int prevX;
    private int prevY;
}