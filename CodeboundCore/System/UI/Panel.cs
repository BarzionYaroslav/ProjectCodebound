namespace Codebound.System.UI;

public class Panel
{
    public ButtonCollection Buttons {
        get { return buttons; }
        set
        {
            if (value != null)
            {
                buttons = value;
            }
            else
                throw new NullReferenceException();
        }
    }
    public string RText
    {
        get { return rtext; }
        set
        {
            if (!value.IsWhiteSpace() && value != null)
            {
                rtext = value;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
    public int Height
    {
        get { return height; }
        set
        {
            if (value > 0)
            {
                height = value;
            }
        }
    }
    
    public int Width2
    {
        get { return width2; }
        set
        {
            if (value > 0)
            {
                width2 = value;
            }
        }
    }
    
    public int Width1
    {
        get { return width1; }
        set
        {
            if (value > 0)
            {
                width1 = value;
            }
        }
    }
    private ButtonCollection buttons = new ButtonCollection();
    private string rtext = "UNOWEN";
    private int height;
    private int width2;
    private int width1;

    public Panel(int width1, int width2, int height, string rtext)
    {
        Width1 = width1;
        Width2 = width2;
        Height = height;
        RText = rtext;
        foreach (var i in DefaultButtons)
        {
            buttons.Add(i);
        }
        Game.KeyPressed += HandleControls;
    }

    public void HandleControls(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                Buttons.SubstractChoice(true);
                break;
            case ConsoleKey.DownArrow:
                Buttons.AddChoice(true);
                break;
            case ConsoleKey.Z:
                Buttons.ExecuteChoice();
                break;
            default:
                break;
        }
    }

    public void DrawUi()
    {
        string text = "";
        int wrdCount = Buttons.Count;
        List<string> rlines = StringToLines(rtext, width2 - 2, height - 4);
        int linecount = rlines.Count;
        for (int i = 0; i < height; i++)
        {
            string msg = "  ";
            string txt = "";
            string col = buttons.GetTextColor((i - 1) / 2);
            if ((i - 1) / 2 < wrdCount && i > 0 && i % 2 == 0)
            {
                msg += Buttons[(i - 1) / 2].Text;
            }
            if ((i - 1) / 2 < linecount && i > 0 && i % 2 == 0)
            {
                txt += rlines[(i - 1) / 2];
            }
            if (i == 0)
                text += "╔" + new string('═', width1 - 1) + "╦" + new string('═', width2 - 1) + "╗\n";
            else if (i == height - 1)
                text += "╚" + new string('═', width1 - 1) + "╩" + new string('═', width2 - 1) + "╝\n";
            else
                text += "║" + col + msg.PadRight(width1 - 1) + "\e[0m║ " + txt.PadRight(width2 - 2) + "║\n";
        }
        Console.Write(text);
    }

    static List<string> StringToLines(string text, int size, int maxLines)
    {
        List<string> answer = new List<string>();
        int left = 0;
        int space;
        while (answer.Count < maxLines)
        {
            if (text.Length - left <= size)
            {
                if (text[left] == ' ')
                    answer.Add(text.Substring(left + 1));
                else
                    answer.Add(text.Substring(left));
                break;
            }
            space = text.LastIndexOf(' ', left + size, size);
            if (space == -1)
            {
                space = left + size;
            }
            if (text[left] == ' ')
                answer.Add(text.Substring(left + 1, space - left));
            else
                answer.Add(text.Substring(left, space - left));
            left = space;
        }
        return answer;
    }

    public static int RunCommand()
    {
        Game.GameStopped = true;
        return 0;
    }

    readonly IEnumerable<Button> DefaultButtons = new List<Button>(
        [
        new Button("FIGHT"),
        new Button("SPELL"),
        new Button("INVENTORY"),
        new Button("DEFEND"),
        new Button("RUN", RunCommand)
        ]
    );
}