namespace Codebound.Items.Weapons;
public class DamageDecorator: WeaponDecorator
{
    public DamageDecorator(BaseWeapon weapon, int dmg) : base(weapon, dmg){}
    public override string GetDescription()
    {
        string text = $"{DecoValue} EXTRA DAMAGE";
        if (DecoValue<0)
            text = $"{Math.Abs(DecoValue)} LESS DAMAGE";
        return _weapon.GetDescription() + "\n" + text;
    }
    public override int GetDamage()
    {
        return _weapon.GetDamage() + DecoValue;
    }
}