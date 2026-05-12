using Codebound.System.UI;
namespace Codebound.System.Control;
public class PanelUpCommand : ICommand
{
    private Panel _target;
    public PanelUpCommand(Panel target)
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
        _target.MoveUpAction();
    }
}