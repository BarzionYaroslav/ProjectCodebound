using Codebound.System;

namespace Codebound.Entities.Opponents;
public class EnemyAttackBuffStrategy: IEnemyActionStrategy
{
    public int Delay { get { return delay; }
        set
        {
            if (value >= 0)
                delay = value;
        } 
    }
    public EnemyAttackBuffStrategy()
    {
        delay = defaultDelay;
    }
    public void Act(Enemy owner)
    {
        SoundManager.PlaySound("buff");
        int random = GameManager.Instance.Randomizer.GetInt(BattleManager.Instance.CurrentWave.Count);
        Enemy target = BattleManager.Instance.CurrentWave[random];
        string textToUse = actionTextOther;
        if (target == owner)
            textToUse = actionTextSelf;
        GameManager.Instance.MainPanel.RText = string.Format(
            textToUse,
            owner.Name,
            target.Name,
            buffAmount);
        target.BaseAtk += buffAmount;
    }
    private int delay;
    private readonly int defaultDelay = 45;
    private readonly int buffAmount = 2;
    private readonly string actionTextOther = "{0} increases {1}'s attack by {2}!";
    private readonly string actionTextSelf = "{0} increased its attack by {2}!";
}