using Codebound.System;

namespace Codebound.Entities.Opponents;
public class EnemySkipStrategy: IEnemyActionStrategy
{
    public int Delay { get { return delay; }
        set
        {
            if (value >= 0)
                delay = value;
        } 
    }
    public EnemySkipStrategy()
    {
        delay = defaultDelay;
    }
    public void Act(Enemy owner)
    {
        SoundManager.PlaySound("CursorMove");
        GameManager.Instance.MainPanel.RText = string.Format(
            actionText,
            owner.Name);
    }
    private int delay;
    private readonly int defaultDelay = 45;
    private readonly string actionText = "{0} just stands there... Menacingly!";
}