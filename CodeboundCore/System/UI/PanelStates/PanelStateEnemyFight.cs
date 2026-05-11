namespace Codebound.System.UI;
public class PanelStateEnemyFight: PanelStateBattleBase
{
    public PanelStateEnemyFight(Panel context) : base(context)
    {
        _activeButtons = _context.SecondaryButtons;
    }
    public override void PrepareUi()
    {
        base.PrepareUi();
        string enemyIconText = BattleManager.Instance.CurrentWave[_context.SecondaryButtons.Choice].Face.GetImageText();
        enemyIcon = [.. enemyIconText.Split('\n')];
        enemyData = [.. BattleManager.Instance.CurrentWave[_context.SecondaryButtons.Choice].ToString().Split('\n')];
        enemyIconWidth = BattleManager.Instance.CurrentWave[_context.SecondaryButtons.Choice].Face.DrawWidth * 2;
        foreach (var i in enemyData)
        {
            if (i.Length > enemyDataWidth)
            {
                enemyDataWidth = i.Length;
            }
        }
        AddLength(enemyDataWidth+enemyDataAddition);
        AddLength(enemyIconWidth);
    }
    public override string DrawUiLoop(int i)
    {
        string text = "";
        text += base.DrawUiLoop(i);
        text += _context.MakePanelPart(i, maxLength - lengthCounter, _context.Height, _context.SecondaryButtons, PanelContinueOptions.Both);
        text += _context.MakePanelPart(i, enemyDataWidth+enemyDataAddition, _context.Height, enemyData, PanelContinueOptions.Both);
        text += _context.MakePanelPart(i, enemyIconWidth, _context.Height, enemyIcon, PanelContinueOptions.Left, false);
        return text;
    }
    public override void BackAction()
    {
        _context.SetState(new PanelStateBattleMain(_context));
    }
    public override void ResetVariables()
    {
        base.ResetVariables();
        enemyIconWidth = 0;
        enemyDataWidth = 0;
    }
    private List<string> enemyData = new List<string>();
    private List<string> enemyIcon = new List<string>();
    private int enemyIconWidth;
    private int enemyDataWidth;
    private readonly int enemyDataAddition = 4;
}