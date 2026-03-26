using Codebound.Drawing;

namespace Codebound.Entities;

public abstract class BaseEntity : IEntity
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

    public virtual void UpdateValues() {}

    public virtual int Hurt(int dmg, bool defIgnore = false)
    {
        int damage = dmg;
        if (!defIgnore)
            damage -= Def;
        if (damage > 0)
        {
            Hp -= damage;
            return damage;
        }
        return 0;
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

    private protected string name = DefaultName;
    private protected int def = DefaultDef;
    private protected int atk = DefaultAtk;
    private protected int hp = DefaultHp;
    private protected int maxHp = DefaultHp;
    private protected Icon face = new Icon(DefaultIcon, 0f);

    const int MaxNameSize = 16;
    const string DefaultName = "??UNOWEN??";
    const int DefaultDef = 0;
    const int DefaultAtk = 0;
    const int DefaultHp = 10;
    const string DefaultIcon = "unowen";
}