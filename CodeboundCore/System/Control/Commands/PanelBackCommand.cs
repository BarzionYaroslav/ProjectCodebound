using Codebound.System.UI;
namespace Codebound.System.Control;
public class PanelBackCommand : ICommand
{
    private Panel _target;
    public PanelBackCommand(Panel target)
    {
        if (target != null)
        {
            _target = target;
        }
        else
            throw new NullReferenceException();
    }
    public void Execute()
    {
        _target.BackAction();
    }
}