namespace Codebound.Entities;

public class Hero: IEntity
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
    public int Def {
        get { return def; }
        set { def = value; } 
        }
    public int Atk {
        get { return atk; }
        set
        {
            if (value >= 0)
                atk = value;
            else
                atk = 0;
        } 
    }
    public int Hp {
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
    
    public int Mana {
        get { return mana; }
        set
        {
            if (value >= 0 && value <= MaxMana)
                mana = value;
            else
                mana = Math.Clamp(value, 0, MaxMana);
        } 
    }
    public int MaxMana
    {
        get { return maxMana; }
        set
        {
            if (value > 0)
                maxMana = value;
            else
                maxMana = 0;
        }
    }
    
    public void UpdateValues(){}

    private string name = DefaultName;
    private int def;
    private int atk;
    private int hp;
    private int maxHp;
    private int mana;
    private int maxMana;

    const int MaxNameSize = 16;
    const string DefaultName = "Rika";
}