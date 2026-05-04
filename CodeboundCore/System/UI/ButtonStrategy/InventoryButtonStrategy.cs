using Codebound.Entities.Opponents;
using Codebound.Entities;

namespace Codebound.System.UI;

public class InventoryButtonStrategy : IButtonStrategy
{
    public void Execute(Panel? panel, int index)
    {
        if (panel != null)
        {
            if (panel.testList.Count != 0)
            {
                SoundManager.PlaySound("CursorMove");
                panel.SecondaryButtons = new ButtonCollection(panel);
                foreach (var i in panel.testList)
                {
                    Button btn;
                    if (i == null)
                        btn = new Button($"Unequip", new WeaponButtonStrategy());
                    else
                        btn = new Button($"{i.GetName()}", new WeaponButtonStrategy());
                    panel.SecondaryButtons.Add(btn);
                }
                panel.state = 2;
            }
            else
                SoundManager.PlaySound("Nuhuh");
        }
    }
}