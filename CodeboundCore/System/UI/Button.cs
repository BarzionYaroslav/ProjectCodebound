namespace Codebound.System.UI;

public class Button
{
    public Func<int> Action
    {
        get { return action; }
        set { action = value; }
    }
    
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

    public Button(string text, Func<int> action)
    {
        this.Text = text;
        this.Action = action;
    }

    public static int DefaultAction()
    {
        return 0;
    }

    private Func<int> action = DefaultAction;
    private string text = "UNOWEN";
    private readonly int MaxTextSize = 16;
}