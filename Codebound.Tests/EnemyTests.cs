using Codebound.Entities.Opponents;
namespace Codebound.Tests;

public class EnemyTests
{
    [Fact]
    public void HealthTest_HealthCantGoLowerThan0()
    {
        Enemy enm = new Enemy("test",0,0,20,20,new Drawing.Sprite());

        enm.Hp -= 30;

        Assert.Equal(0, enm.Hp);
    }

    [Fact]
    public void HealthTest_HealthCantGoHigherThanMax()
    {
        Enemy enm = new Enemy("test", 0, 0, 20, 20, new Drawing.Sprite());

        enm.Hp += 30;

        Assert.Equal(enm.MaxHp, enm.Hp);
    }

    [Fact]
    public void HurtTest_NegativeDamageDoesChipDamage()
    {
        Enemy enm = new Enemy("test", 0, 0, 20, 20, new Drawing.Sprite());

        enm.Hurt(-1, true);

        Assert.Equal(enm.MaxHp - 1, enm.Hp);
    }

    [Fact]
    public void HurtTest_ZeroDamageDoesChipDamage()
    {
        Enemy enm = new Enemy("test", 0, 0, 20, 20, new Drawing.Sprite());

        enm.Hurt(0, true);

        Assert.Equal(enm.MaxHp - 1, enm.Hp);
    }

    [Fact]
    public void HurtTest_DamageCalculatesProperly()
    {
        Enemy enm = new Enemy("test", 0, 0, 20, 20, new Drawing.Sprite());

        enm.Hurt(5, true);

        Assert.Equal(enm.MaxHp - 5, enm.Hp);
    }

    [Fact]
    public void HurtTest_DefenseTests_DefenseBlocksDamageLessThanItButLeavesChip()
    {
        Enemy enm = new Enemy("test", 5, 0, 20, 20, new Drawing.Sprite());

        enm.Hurt(4);

        Assert.Equal(enm.MaxHp - 1, enm.Hp);
    }

    [Fact]
    public void HurtTest_DefenseTests_DefenseBlocksDamageEqualToItButLeavesChip()
    {
        Enemy enm = new Enemy("test", 5, 0, 20, 20, new Drawing.Sprite());

        enm.Hurt(5);

        Assert.Equal(enm.MaxHp - 1, enm.Hp);
    }

    [Fact]
    public void HurtTest_DefenseTests_DefenseBlocksPartOfTheDamageMoreThanIt()
    {
        Enemy enm = new Enemy("test", 5, 0, 20, 20, new Drawing.Sprite());

        enm.Hurt(8);

        Assert.Equal(enm.MaxHp - 3, enm.Hp);
    }

    [Fact]
    public void HurtTest_CantGoLowerThan0()
    {
        Enemy enm = new Enemy("test", 0, 0, 20, 20, new Drawing.Sprite());

        enm.Hurt(90, true);

        Assert.Equal(0, enm.Hp);
    }

    [Fact]
    public void HealTest_HealthIncreasesNormally()
    {
        Enemy enm = new Enemy("test", 5, 0, 15, 20, new Drawing.Sprite());

        enm.Heal(5);

        Assert.Equal(enm.MaxHp, enm.Hp);
    }

    [Fact]
    public void HealTest_HealthCantGoPastMax()
    {
        Enemy enm = new Enemy("test", 5, 0, 15, 20, new Drawing.Sprite());

        enm.Heal(20);

        Assert.Equal(enm.MaxHp, enm.Hp);
    }

    [Fact]
    public void HealTest_CantRevive()
    {
        Enemy enm = new Enemy("test", 5, 0, 0, 20, new Drawing.Sprite());

        enm.Heal(20);

        Assert.Equal(0, enm.Hp);
    }
    
    [Fact]
    public void HealTest_DoesNothingOnNegative()
    {
        Enemy enm = new Enemy("test", 5, 0, 15, 20, new Drawing.Sprite());

        enm.Heal(-5);

        Assert.Equal(15, enm.Hp);
    }
}