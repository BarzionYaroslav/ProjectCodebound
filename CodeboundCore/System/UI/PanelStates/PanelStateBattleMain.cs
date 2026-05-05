namespace Codebound.System.UI;
public class PanelStateBattleMain: PanelStateBattleBase
{
    public PanelStateBattleMain(Panel context) : base(context) { }
    public override void PrepareUi()
    {
        base.PrepareUi();
        textWidth = MaxLength - lengthCounter;
        textLines = Panel.StringToLines(_context.RText, textWidth, _context.Height - 4);
        AddLength(textWidth);
    }
    public override string DrawUiLoop(int i)
    {
        string text = "";
        text += base.DrawUiLoop(i);
        text += _context.MakePanelPart(i, textWidth, _context.Height, textLines, PanelContinueOptions.Left);
        return text;
    }
    public override void ResetVariables()
    {
        base.ResetVariables();
        textWidth = 0;
    }
    private List<string> textLines = new List<string>();
    private int textWidth;
}