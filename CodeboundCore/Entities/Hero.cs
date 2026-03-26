using Codebound.Drawing;

namespace Codebound.Entities;

public class Hero: BaseEntity
{
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

    public override void UpdateValues() { }

    public override string ToString()
    {
        return $"Name: {name}\nDEF: {def}\nATK: {atk}\nHP: {hp}/{maxHp}\nMANA: {mana}/{maxMana}";
    }
    private int mana = DefaultMana;
    private int maxMana = DefaultMana;
    const int DefaultMana = 10;
}