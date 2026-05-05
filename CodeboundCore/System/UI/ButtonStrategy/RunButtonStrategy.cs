using Codebound.Entities.Opponents;
using Codebound.Entities;

namespace Codebound.System.UI;

public class RunButtonStrategy : IButtonStrategy
{
    public void Execute(Panel? panel, int index)
    {
        if (panel != null)
            panel.RText = "And so, you ran away...";
        GameManager.Instance.EndGame();
    }
}