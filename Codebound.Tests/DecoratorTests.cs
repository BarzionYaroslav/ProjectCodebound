using Codebound.Entities;
using Codebound.Entities.Opponents;
using Codebound.Items.Weapons;
namespace Codebound.Tests;

public class DecoratorTests
{
    [Fact]
    public void DamageTest_DamageDecoratorAddsDamage()
    {
        Sword sword = new Sword("Tester", "Sword for testings", 5);

        BaseWeapon decoSword = new DamageDecorator(sword, 5);

        Assert.Equal(10, decoSword.GetDamage());
    }
    [Fact]
    public void DamageTest_DamageDecoratorLowersDamage()
    {
        Sword sword = new Sword("Tester", "Sword for testings", 5);

        BaseWeapon decoSword = new DamageDecorator(sword, -2);

        Assert.Equal(3, decoSword.GetDamage());
    }
    [Fact]
    public void HealthTest_HealthIsDrainedOnUse()
    {
        Hero hero = new HeroBuilder().SetHp(25).Build();
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).Build();
        Sword sword = new Sword("Tester", "Sword for testings", 5);

        BaseWeapon decoSword = new HealthDecorator(sword, -5);
        decoSword.Use(hero, enm);

        Assert.Equal(20, hero.Hp);
    }
    [Fact]
    public void HealthTest_HealthAddedOnUse()
    {
        Hero hero = new HeroBuilder().SetHp(25).Build();
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).Build();
        Sword sword = new Sword("Tester", "Sword for testings", 5);

        hero.Hurt(10, true);
        BaseWeapon decoSword = new HealthDecorator(sword, 5);
        decoSword.Use(hero, enm);

        Assert.Equal(20, hero.Hp);
    }
    [Fact]
    public void DamageTest_DecoratorChainAddsAllDamagesUp_OnlyPositives()
    {
        Sword sword = new Sword("Tester", "Sword for testings", 5);

        BaseWeapon decoSword = new DamageDecorator(new DamageDecorator(new DamageDecorator(sword, 5), 5), 5);

        Assert.Equal(20, decoSword.GetDamage());
    }
    [Fact]
    public void DamageTest_DecoratorChainAddsAllDamagesUp_WithNegatives()
    {
        Sword sword = new Sword("Tester", "Sword for testings", 5);

        BaseWeapon decoSword = new DamageDecorator(new DamageDecorator(new DamageDecorator(sword, 5), -5), 5);

        Assert.Equal(10, decoSword.GetDamage());
    }
    [Fact]
    public void DamageTest_DecoratorChainAddsAllDamagesUp_OnlyNegatives()
    {
        Sword sword = new Sword("Tester", "Sword for testings", 30);

        BaseWeapon decoSword = new DamageDecorator(new DamageDecorator(new DamageDecorator(sword, -5), -5), -5);

        Assert.Equal(15, decoSword.GetDamage());
    }

    [Fact]
    public void HeathDamageTest_DecoratorChainAddsDamageAndDrainsHealth()
    {
        Hero hero = new HeroBuilder().SetHp(25).Build();
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).Build();
        Sword sword = new Sword("Tester", "Sword for testings", 5);

        BaseWeapon decoSword = new HealthDecorator(new DamageDecorator(new DamageDecorator(sword, 5), 10), -5);
        decoSword.Use(hero, enm);

        Assert.Equal(20, decoSword.GetDamage());
        Assert.Equal(20, hero.Hp);
    }
}