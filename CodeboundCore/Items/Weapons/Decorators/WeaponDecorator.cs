using Codebound.Entities;
using Codebound.Entities.Opponents;
namespace Codebound.Items.Weapons;

public abstract class WeaponDecorator : BaseWeapon
{
    public int DecoValue
    {
        get { return decoValue; }
        set { decoValue = value; }
    }
    protected BaseWeapon _weapon;
    public WeaponDecorator(BaseWeapon weapon, int value)
    {
        if (weapon == null)
            throw new NullReferenceException();
        _weapon = weapon;
        DecoValue = value;
    }
    public override string GetDescription()
    {
        return _weapon.GetDescription();
    }
    public override string GetName()
    {
        return _weapon.GetName();
    }
    public override int GetDamage()
    {
        return _weapon.GetDamage();
    }
    public override void Use(Hero user, Enemy target)
    {
        _weapon.Use(user, target);
    }
    private int decoValue;
}