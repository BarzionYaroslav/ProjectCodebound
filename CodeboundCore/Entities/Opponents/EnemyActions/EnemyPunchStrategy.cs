using Codebound.System;

namespace Codebound.Entities.Opponents;
public class EnemyPunchStrategy: IEnemyActionStrategy
{
    public int Delay { get { return delay; }
        set
        {
            if (value >= 0)
                delay = value;
        } 
    }
    public EnemyPunchStrategy()
    {
        delay = defaultDelay;
    }
    public void Act(Enemy owner)
    {
        SoundManager.PlaySound("punch");
        int damage = BattleManager.Instance.MainChar.Hurt(owner.Atk);
        GameManager.Instance.MainPanel.RText = string.Format(
            actionText,
            owner.Name,
            BattleManager.Instance.MainChar.Name,
            damage);
    }
    private int delay;
    private readonly int defaultDelay = 45;
    private readonly string actionText = "{0} attacked {1} for {2} points of damage!";
}