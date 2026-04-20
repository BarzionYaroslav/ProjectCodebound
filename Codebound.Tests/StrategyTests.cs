using Codebound.Entities;
using Codebound.Entities.Opponents;
using Codebound.System;
using Codebound.System.Randomness;
namespace Codebound.Tests;

public class StrategyTests
{
    [Fact]
    public void SkipStrategy_UI_ChangesText()
    {
        Enemy enemy = new EnemyBuilder<Ibiruai>()
                    .SetName("Enemy")
                    .Build();
        enemy.SetAction(new EnemySkipStrategy());
        enemy.DoAction();
        Assert.Equal("Enemy just stands there... Menacingly!", GameManager.Instance.MainPanel.RText);
    }

    [Fact]
    public void EnemyPunchStrategy_UI_ChangesText()
    {
        Enemy enemy = new EnemyBuilder<Ibiruai>()
                    .SetName("Enemy")
                    .SetAtk(10)
                    .Build();
        Hero hero = BattleManager.Instance.MainChar;
        enemy.SetAction(new EnemyPunchStrategy());
        enemy.DoAction();
        Assert.Equal($"Enemy attacked {hero.Name} for {enemy.Atk - hero.Def} points of damage!", GameManager.Instance.MainPanel.RText);
    }

    [Fact]
    public void EnemyPunchStrategy_Action_LowersHealth()
    {
        Enemy enemy = new EnemyBuilder<Ibiruai>()
                    .SetName("Enemy")
                    .SetAtk(10)
                    .Build();
        Hero hero = BattleManager.Instance.MainChar;
        int oldHp = hero.Hp;
        enemy.SetAction(new EnemyPunchStrategy());
        enemy.DoAction();
        Assert.Equal(oldHp - (enemy.Atk - hero.Def), hero.Hp);
    }
}