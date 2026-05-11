namespace Codebound.System.Control;
public class GameEndCommand: ICommand
{
    public void Execute()
    {
        GameManager.Instance.EndGame();
    }
}