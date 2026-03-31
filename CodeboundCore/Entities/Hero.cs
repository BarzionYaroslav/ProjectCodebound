using Codebound.Drawing;
using Codebound.Items;
using Codebound.Items.Weapons;

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

    public BaseWeapon? Weapon
    {
        get { return weapon; }
        set
        {
            weapon = value;
        }
    }

    public override void UpdateValues() { }

    public override int GetDamage()
    {
        if (weapon!=null)
        {
            return BaseAtk + weapon.GetDamage();
        }
        return BaseAtk;
    }
    public override int GetDefense()
    {
        return BaseDef;
    }

    public override string ToString()
    {
        return $"Name: {name}\nDEF: {Def}\nATK: {Atk}\nHP: {hp}/{maxHp}\nMANA: {mana}/{maxMana}";
    }
    private BaseWeapon? weapon = null;
    private IItem? armor = null;
    private int mana = DefaultMana;
    private int maxMana = DefaultMana;
    const int DefaultMana = 10;
}