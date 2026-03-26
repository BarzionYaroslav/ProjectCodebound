using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class Enemy : BaseEntity
{
    public Sprite Body
    {
        get { return body; }
        set
        {
            if (value != null)
                body = value;
            else
                throw new NullReferenceException();
        }
    }

    public Enemy()
    {
        this.body = new Sprite();
    }

    public Enemy(string name, int def, int atk, int hp, int maxHp, Sprite body, Icon face)
    {
        Name = name;
        Def = def;
        Atk = atk;
        MaxHp = maxHp;
        Hp = hp;
        if (body != null)
            this.body = body;
        else
            this.body = new Sprite();
        if (face != null)
            this.face = face;
        GameManager.UpdateStarted += UpdateValues;
    }
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
    private Sprite body;
}