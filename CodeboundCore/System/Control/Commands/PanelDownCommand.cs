using Codebound.System.UI;
namespace Codebound.System.Control;
public class PanelDownCommand : ICommand
{
    private Panel _target;
    public PanelDownCommand(Panel target)
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
        _target.MoveDownAction();
    }
}