using Codebound.Drawing;
using Codebound.System;
namespace Codebound.Entities.Opponents;

public class Enemy : IEntity
{
    public string Name
    {
        get { return name; }
        set
        {
            if (!value.IsWhiteSpace() && value != null)
            {
                if (value.Length <= MaxNameSize)
                    name = value;
                else
                    throw new ArithmeticException();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
    public int Def
    {
        get { return def; }
        set
        {
            if (value >= 0)
                def = value;
            else
                def = 0;
        }
    }
    public int Atk
    {
        get { return atk; }
        set
        {
            if (value >= 0)
                atk = value;
            else
                atk = 0;
        }
    }
    public int Hp
    {
        get { return hp; }
        set
        {
            if (value >= 0 && value <= MaxHp)
                hp = value;
            else
                hp = Math.Clamp(value, 0, MaxHp);
        }
    }
    public int MaxHp
    {
        get { return maxHp; }
        set
        {
            if (value > 0)
                maxHp = value;
            else
                maxHp = 0;
        }
    }
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

    public Icon Face
    {
        get { return face; }
        set
        {
            if (value != null)
                face = value;
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

    public virtual void UpdateValues() { }
    public int Hurt(int dmg, bool defIgnore = false)
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
    public int Heal(int value)
    {
        if (Hp != 0 && value > 0)
        {
            Hp += value;
            return value;
        }
        return 0;
    }

    public override string ToString()
    {
        return $"Name: {name}\nDEF: {def}\nATK: {atk}\nHP: {hp}/{maxHp}\n";
    }

    private string name = DefaultName;
    private int def = 0;
    private int atk = 0;
    private int hp = 10;
    private int maxHp = 10;
    private Sprite body;
    private Icon face = new Icon(DefaultIcon, 0f);

    const int MaxNameSize = 16;
    const string DefaultName = "Punchy Bag";
    const string DefaultIcon = "punchy";
}