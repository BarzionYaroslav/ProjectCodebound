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
    //Yes, defense can go into negatives. Yes, it's intentional
    public int Def
    {
        get { return def; }
        set { def = value; }
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

    public Enemy(string name, int def, int atk, int hp, int maxHp, Sprite body)
    {
        Name = name;
        Def = def;
        Atk = atk;
        MaxHp = maxHp;
        Hp = hp;
        this.body = body;
        Game.UpdateStarted += UpdateValues;
    }

    public virtual void UpdateValues(){}

    private string name = DefaultName;
    private int def;
    private int atk;
    private int hp;
    private int maxHp;
    private Sprite body;

    const int MaxNameSize = 16;
    const string DefaultName = "Punchy Bag";
}