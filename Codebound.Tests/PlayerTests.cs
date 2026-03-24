using Codebound.Entities;
namespace Codebound.Tests;

public class PlayerTests
{

    [Fact]
    public void HealthTest_HealthCantGoLowerThan0()
    {
        Hero hero = new HeroBuilder().SetHp(25)
            .Build();

        hero.Hp -= 30;

        Assert.Equal(0, hero.Hp);
    }

    [Fact]
    public void HealthTest_HealthCantGoHigherThanMax()
    {
        Hero hero = new HeroBuilder().SetHp(25)
            .Build();

        hero.Hp += 30;

        Assert.Equal(hero.MaxHp, hero.Hp);
    }

    [Fact]
    public void ManaTest_ManaCantGoLowerThan0()
    {
        Hero hero = new HeroBuilder().SetMana(25)
            .Build();

        hero.Mana -= 30;

        Assert.Equal(0, hero.Mana);
    }

    [Fact]
    public void ManaTest_ManaCantGoHigherThanMax()
    {
        Hero hero = new HeroBuilder().SetMana(25)
            .Build();

        hero.Mana += 30;

        Assert.Equal(hero.MaxMana, hero.Mana);
    }

    [Fact]
    public void HurtTest_NegativeDamageDoesNothing()
    {
        int startingHealth = 25;
        Hero hero = new HeroBuilder().SetHp(startingHealth)
            .Build();

        hero.Hurt(-10);

        Assert.Equal(startingHealth, hero.Hp);
    }

    [Fact]
    public void HurtTest_ZeroDamageDoesNothing()
    {
        int startingHealth = 25;
        Hero hero = new HeroBuilder().SetHp(startingHealth)
            .Build();

        hero.Hurt(0);

        Assert.Equal(startingHealth, hero.Hp);
    }

    [Fact]
    public void HurtTest_HurtsHeroForSpecifiedDamage()
    {
        int startingHealth = 25;
        Hero hero = new HeroBuilder().SetHp(startingHealth)
            .Build();

        hero.Hurt(10);

        Assert.Equal(startingHealth - 10, hero.Hp);
    }

    [Fact]
    public void HurtTest_DefenseTests_DefenseBlocksDamageEqualToIt()
    {
        int startingHealth = 25;
        int defense = 5;
        Hero hero = new HeroBuilder().SetHp(startingHealth)
            .SetDef(defense)
            .Build();

        hero.Hurt(defense);

        Assert.Equal(startingHealth, hero.Hp);
    }

    [Fact]
    public void HurtTest_DefenseTests_DefenseBlocksDamageLessThanIt()
    {
        int startingHealth = 25;
        int defense = 5;
        Hero hero = new HeroBuilder().SetHp(startingHealth)
            .SetDef(defense)
            .Build();

        hero.Hurt(defense / 2);

        Assert.Equal(startingHealth, hero.Hp);
    }

    [Fact]
    public void HurtTest_DefenseTests_DefenseBlocksPartOfDamageMoreThanIt()
    {
        int startingHealth = 25;
        int defense = 5;
        Hero hero = new HeroBuilder().SetHp(startingHealth)
            .SetDef(defense)
            .Build();

        hero.Hurt(defense * 2);

        Assert.Equal(startingHealth - defense, hero.Hp);
    }

    [Fact]
    public void HurtTest_DefenseTests_DefenseIgnoreIsSameAsZeroDefense()
    {
        int startingHealth = 25;
        int defense = 5;
        int damage = 5;
        Hero defendedHero = new HeroBuilder().SetHp(startingHealth)
            .SetDef(defense)
            .Build();
        Hero undefendedHero = new HeroBuilder().SetHp(startingHealth)
            .SetDef(0)
            .Build();

        defendedHero.Hurt(damage, true);
        undefendedHero.Hurt(damage);

        Assert.Equal(undefendedHero.Hp, defendedHero.Hp);
    }

    [Fact]
    public void HurtTest_CantGoLowerThan0()
    {
        int startingHealth = 25;
        Hero hero = new HeroBuilder().SetHp(startingHealth)
            .Build();

        hero.Hurt(90);

        Assert.Equal(0, hero.Hp);
    }

    [Fact]
    public void HealTest_HealthIncreasesNormally()
    {
        int startingHealth = 25;
        Hero hero = new HeroBuilder().SetHp(startingHealth)
            .Build();

        hero.Hurt(10, true);
        hero.Heal(5);

        Assert.Equal(startingHealth - 5, hero.Hp);
    }

    [Fact]
    public void HealTest_HealthCantGoPastMax()
    {
        int startingHealth = 25;
        Hero hero = new HeroBuilder().SetHp(startingHealth)
            .Build();

        hero.Heal(5);

        Assert.Equal(startingHealth, hero.Hp);
    }

    [Fact]
    public void HealTest_CantRevive()
    {
        int startingHealth = 25;
        Hero hero = new HeroBuilder().SetHp(startingHealth)
            .Build();

        hero.Hurt(startingHealth);
        hero.Heal(5);

        Assert.Equal(0, hero.Hp);
    }
    
    [Fact]
    public void HealTest_DoesNothingOnNegative()
    {
        int startingHealth = 25;
        Hero hero = new HeroBuilder().SetHp(startingHealth)
            .Build();

        hero.Heal(-5);

        Assert.Equal(startingHealth, hero.Hp);
    }
}
