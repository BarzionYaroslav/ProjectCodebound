namespace Codebound.System.Control;
public class WaveRerollCommand: ICommand
{
    public void Execute()
    {
        BattleManager.Instance.RerollWave();
    }
}