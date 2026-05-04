using Codebound.Entities.Opponents;
namespace Codebound.System.UI;

public class Button
{
    public string Text
    {
        get { return text; }
        set
        {
            if (!value.IsWhiteSpace() && value != null)
            {
                if (value.Length <= MaxTextSize)
                    text = value;
                else
                    throw new ArithmeticException();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }

    public Button(string text)
    {
        this.Text = text;
    }
    public Button(string text, IButtonStrategy context)
    {
        this.Text = text;
        this._strategy = context;
    }

    public void Execute(Panel? panel, int index)
    {
        if (_strategy == null)
        {
            SoundManager.PlaySound(soundPath);
        }
        else
        {
            _strategy.Execute(panel, index);
        }
    }

    public void SetStrategy(IButtonStrategy context)
    {
        _strategy = context;
    }

    private IButtonStrategy? _strategy;
    private string text = "UNOWEN";
    private readonly int MaxTextSize = 32;
    private static readonly string soundPath = "Nuhuh";
}