using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class Enemy : BaseEntity
{
    public ComplexSpriter Body
    {
        get { return body; }
        set
        {
            if (value != null)
            {
                body.Dispose();
                body = value;
            }
            else
                throw new NullReferenceException();
        }
    }

    public Enemy()
    {
        Expectations.Add(BodyName);
        this.body = new ComplexSpriter(Expectations);
        GameManager.UpdateStarted += UpdateValues;
    }

    public virtual void AfterPrep() { }

    public override int Hurt(int dmg, bool defIgnore = false)
    {
        int damage = dmg;
        if (!defIgnore)
            damage -= Def;
        if (damage > 0)
        {
            Hp -= damage;
            return damage;
        }
        else
        {
            Hp -= 1;
            return 1;
        }
    }
    private ComplexSpriter body;
    public HashSet<string> Expectations { get; private set; } = new HashSet<string>();
    static public readonly string BodyName = "body";
}