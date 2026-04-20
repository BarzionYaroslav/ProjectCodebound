using Codebound.System;

namespace Codebound.Entities.Opponents;
public class EnemyStrongPunchStrategy: IEnemyActionStrategy
{
    public int Delay { get { return delay; }
        set
        {
            if (value >= 0)
                delay = value;
        } 
    }
    public EnemyStrongPunchStrategy()
    {
        delay = defaultDelay;
    }
    public void Act(Enemy owner)
    {
        int damage = 0;
        string textToUse = actionText;
        string soundToUse = successSound;
        if (GameManager.Instance.Randomizer.GetDouble() > missChance)
        {
            damage = BattleManager.Instance.MainChar.Hurt((int)Math.Floor(owner.Atk * attackMultiplier));
            textToUse += successText;
        }
        else
        {
            textToUse += failText;
            soundToUse = failSound;
        }
        SoundManager.PlaySound(soundToUse);
        GameManager.Instance.MainPanel.RText = string.Format(
            textToUse,
            owner.Name,
            BattleManager.Instance.MainChar.Name,
            damage);
    }
    private int delay;
    private float attackMultiplier = 1.2f;
    private float missChance = 0.25f;
    private readonly int defaultDelay = 45;
    private readonly string actionText = "{0} tries a strong attack on {1}... ";
    private readonly string failText = "But it missed!";
    private readonly string successText = "It hits for {2} points of damage!";
    private readonly string failSound = "Nuhuh";
    private readonly string successSound = "punchStrong";
}