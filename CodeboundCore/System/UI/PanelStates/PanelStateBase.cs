namespace Codebound.System.UI;
public abstract class PanelStateBase: IPanelState
{
    protected Panel _context;
    public int LengthCounter {get { return lengthCounter; }}
    public int MaxLength {get { return maxLength; }}
    public PanelStateBase(Panel context)
    {
        _context = context;
        maxLength = context.Width;
    }
    public void DrawUi()
    {
        PrepareUi();
        string text = "";
        for (int i = 0; i < _context.Height; i++)
        {
            text += DrawUiLoop(i);
        }
        Console.Write(text);
        ResetVariables();
    }
    public abstract void PrepareUi();
    public abstract string DrawUiLoop(int i);
    public abstract void HandleControls(ConsoleKey key);
    public void AddLength(int length)
    {
        lengthCounter += length + borderSize;
    }
    public void SetContext(Panel context)
    {
        _context = context;
    }
    public virtual void ResetVariables()
    {
        lengthCounter = 0;
    }

    protected int lengthCounter = 0;
    protected int maxLength;
    private int borderSize = 2;
}