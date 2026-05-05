using Codebound.Entities.Opponents;

namespace Codebound.System.UI;

public class FightButtonStrategy : IButtonStrategy
{
    public void Execute(Panel? panel, int index)
    {
        if (panel != null)
        {
            if (BattleManager.Instance.CurrentWave.Count != 0)
            {
                SoundManager.PlaySound("CursorMove");
                panel.SecondaryButtons = new ButtonCollection(panel);
                foreach (Enemy i in BattleManager.Instance.CurrentWave)
                {
                    Button btn = new Button($"{i.Name}", new AttackButtonStrategy());
                    panel.SecondaryButtons.Add(btn);
                }
                panel.SetState(new PanelStateEnemyFight(panel));
            }
            else
                SoundManager.PlaySound("Nuhuh");
        }
    }
}