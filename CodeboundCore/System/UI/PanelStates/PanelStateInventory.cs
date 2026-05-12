namespace Codebound.System.UI;
public class PanelStateInventory: PanelStateBattleBase
{
    public PanelStateInventory(Panel context) : base(context)
    {
        _activeButtons = _context.SecondaryButtons;
    }
    public override void PrepareUi()
    {
        base.PrepareUi();
        List<string> description;
        var weapon = _context.testList[_context.SecondaryButtons.Choice];
        if (weapon != null)
            description = [.. weapon.GetDescription().Split('\n')];
        else
            description = new List<string>(["Unequip your weapon"]);
        descriptionLines = Panel.StringToLines(description, descriptionWidth, _context.Height - 2);
        AddLength(descriptionWidth);
    }
    public override string DrawUiLoop(int i)
    {
        string text = "";
        text += base.DrawUiLoop(i);
        text += _context.MakePanelPart(i, _context.Width - lengthCounter, _context.Height, _context.SecondaryButtons, PanelContinueOptions.Both);
        text += _context.MakePanelPart(i, descriptionWidth, _context.Height, descriptionLines, PanelContinueOptions.Left);
        return text;
    }
    public override void BackAction()
    {
        _context.SetState(new PanelStateBattleMain(_context));
    }
    private List<string> descriptionLines = new List<string>();
    private int descriptionWidth = 76;
}