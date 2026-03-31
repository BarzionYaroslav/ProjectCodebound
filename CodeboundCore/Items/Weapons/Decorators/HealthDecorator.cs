using Codebound.Entities;
using Codebound.Entities.Opponents;
namespace Codebound.Items.Weapons;

public class HealthDecorator : WeaponDecorator
{
    public HealthDecorator(BaseWeapon weapon, int val) : base(weapon, val) { }
    public override string GetDescription()
    {
        string text = $"HEALS {DecoValue} HP ON USE";
        if (DecoValue < 0)
            text = $"DRAINS {Math.Abs(DecoValue)} HP ON USE";
        return _weapon.GetDescription() + "\n" + text;
    }
    public override int GetDamage()
    {
        return _weapon.GetDamage();
    }
    public override void Use(Hero user, Enemy target)
    {
        _weapon.Use(user, target);
        if (DecoValue < 0)
            user.Hurt(Math.Abs(DecoValue), true);
        else
            user.Heal(DecoValue);
    }
}