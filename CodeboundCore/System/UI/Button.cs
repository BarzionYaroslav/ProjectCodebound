using NetCoreAudio;
namespace Codebound.System.UI;

public delegate void ButtonAction(Panel panel);
public class Button
{
    public ButtonAction Action
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

    public Button(string text, ButtonAction action)
    {
        this.Text = text;
        this.Action = action;
    }

    public static void DefaultAction(Panel panel)
    {
        if (!sounder.Playing)
            sounder.Play(soundPath);
    }

    private ButtonAction action = DefaultAction;
    private string text = "UNOWEN";
    private readonly int MaxTextSize = 16;
    private static readonly Player sounder = new Player();
    private static readonly string soundPath = @"./assets/sounds/Nuhuh.wav";
}