using Codebound.Entities.Opponents;
using Codebound.Entities;

namespace Codebound.System.UI;

public class AttackButtonStrategy : IButtonStrategy
{
    public void Execute(Panel? panel, int index)
    {
        if (panel != null)
        {
            Wave wave = BattleManager.Instance.CurrentWave;
            Enemy enm = wave[index];
            Hero character = BattleManager.Instance.MainChar;
            if (character.Weapon != null)
                character.Weapon.Use(character, enm);
            else
            {
                SoundManager.PlaySound("punch");
                BattleManager.Instance.AddEffect(index, "punch_fx", 1f);
            }
            int dmg = enm.Hurt(character.Atk);
            panel.RText = $"You attacked {enm.Name} for {dmg} HP! It didn't really like that!";
            panel.SetState(new PanelStateBattleMain(panel));
            BattleManager.Instance.StartEnemyTurn();
        }
    }
}