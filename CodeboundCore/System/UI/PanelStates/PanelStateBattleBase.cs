using Codebound.Entities;

namespace Codebound.System.UI;
public abstract class PanelStateBattleBase: PanelStateBase
{
    public PanelStateBattleBase(Panel context) : base(context) { }
    public override void PrepareUi()
    {
        var icon = BattleManager.Instance.MainChar.Face;
        string iconText = icon.GetImageText();
        iconLines = [.. iconText.Split('\n')];
        iconWidth = icon.DrawWidth * 2;
        playerData = [.. BattleManager.Instance.MainChar.ToString().Split('\n')];
        foreach (var i in playerData)
        {
            if (i.Length > maxDataWidth)
            {
                maxDataWidth = i.Length;
            }
        }
        AddLength(iconWidth);
        AddLength(maxDataWidth + dataWidthAddition);
        AddLength(buttonWidth);
    }
    public override string DrawUiLoop(int i)
    {
        string text = "";
        text += _context.MakePanelPart(i, iconWidth, _context.Height, iconLines, PanelContinueOptions.Right, false);
        text += _context.MakePanelPart(i, maxDataWidth + dataWidthAddition, _context.Height, playerData, PanelContinueOptions.Both);
        text += _context.MakePanelPart(i, buttonWidth, _context.Height, _context.Buttons, PanelContinueOptions.Both);
        return text;
    }
    public override void ResetVariables()
    {
        base.ResetVariables();
        iconWidth = 0;
        maxDataWidth = 0;
    }

    private int iconWidth;
    private int maxDataWidth = 0;
    private List<string> playerData = new List<string>();
    private List<string> iconLines = new List<string>();
    private readonly int dataWidthAddition = 4;
    private readonly int buttonWidth = 12;
}