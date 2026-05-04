using Codebound.Entities.Opponents;
using Codebound.Entities;

namespace Codebound.System.UI;

public class WeaponButtonStrategy : IButtonStrategy
{
    public void Execute(Panel? panel, int index)
    {
        if (panel != null)
        {
            Hero character = BattleManager.Instance.MainChar;
            character.Weapon = panel.testList[index];
            panel.SecondaryButtons = null;
            panel.state = 0;
            BattleManager.Instance.StartEnemyTurn();
        }
    }
}