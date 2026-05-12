using Codebound.System.UI;
namespace Codebound.System.Control;
public class PanelSelectCommand : ICommand
{
    private Panel _target;
    public PanelSelectCommand(Panel target)
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
        _target.SelectAction();
    }
}