using Codebound.Entities.Opponents;
namespace Codebound.Tests;

public class EnemyTests
{
    [Fact]
    public void HealthTest_HealthCantGoLowerThan0()
    {
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).Build();

        enm.Hp -= 30;

        Assert.Equal(0, enm.Hp);
    }

    [Fact]
    public void HealthTest_HealthCantGoHigherThanMax()
    {
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).Build();

        enm.Hp += 30;

        Assert.Equal(enm.MaxHp, enm.Hp);
    }

    [Fact]
    public void HurtTest_NegativeDamageDoesChipDamage()
    {
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).Build();

        enm.Hurt(-1, true);

        Assert.Equal(enm.MaxHp - 1, enm.Hp);
    }

    [Fact]
    public void HurtTest_ZeroDamageDoesChipDamage()
    {
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).Build();

        enm.Hurt(0, true);

        Assert.Equal(enm.MaxHp - 1, enm.Hp);
    }

    [Fact]
    public void HurtTest_DamageCalculatesProperly()
    {
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).Build();

        enm.Hurt(5, true);

        Assert.Equal(enm.MaxHp - 5, enm.Hp);
    }

    [Fact]
    public void HurtTest_DefenseTests_DefenseBlocksDamageLessThanItButLeavesChip()
    {
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).SetDef(5).Build();

        enm.Hurt(4);

        Assert.Equal(enm.MaxHp - 1, enm.Hp);
    }

    [Fact]
    public void HurtTest_DefenseTests_DefenseBlocksDamageEqualToItButLeavesChip()
    {
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).SetDef(5).Build();

        enm.Hurt(5);

        Assert.Equal(enm.MaxHp - 1, enm.Hp);
    }

    [Fact]
    public void HurtTest_DefenseTests_DefenseBlocksPartOfTheDamageMoreThanIt()
    {
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).SetDef(5).Build();

        enm.Hurt(8);

        Assert.Equal(enm.MaxHp - 3, enm.Hp);
    }

    [Fact]
    public void HurtTest_CantGoLowerThan0()
    {
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).Build();

        enm.Hurt(90, true);

        Assert.Equal(0, enm.Hp);
    }

    [Fact]
    public void HealTest_HealthIncreasesNormally()
    {
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).Build();

        enm.Hp -= 5;
        enm.Heal(5);

        Assert.Equal(enm.MaxHp, enm.Hp);
    }

    [Fact]
    public void HealTest_HealthCantGoPastMax()
    {
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).SetDef(5).Build();

        enm.Hp -= 5;
        enm.Heal(20);

        Assert.Equal(enm.MaxHp, enm.Hp);
    }

    [Fact]
    public void HealTest_CantRevive()
    {
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).SetDef(5).Build();

        enm.Hp = 0;
        enm.Heal(20);

        Assert.Equal(0, enm.Hp);
    }
    
    [Fact]
    public void HealTest_DoesNothingOnNegative()
    {
        Enemy enm = new EnemyBuilder<Enemy>().SetHp(20).SetDef(5).Build();

        enm.Hp -= 5;
        enm.Heal(-5);

        Assert.Equal(15, enm.Hp);
    }
}