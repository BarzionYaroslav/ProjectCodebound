using ImageMagick;
using Codebound.Drawing;
using Codebound.Entities.Opponents;
namespace Codebound.System.UI;

public class Panel
{
    private Icon rik = new Icon("rika",0);
    public ButtonCollection Buttons
    {
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
    public ButtonCollection? SecondaryButtons {
        get { return secondaryButtons; }
        set
        {
            secondaryButtons = value;
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
    private ButtonCollection? secondaryButtons;
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
        buttons.Link = this;
        GameManager.KeyPressed += HandleControls;
    }

    public void HandleControls(ConsoleKey key)
    {
        ButtonCollection curButtons;
        if (secondaryButtons == null)
            curButtons = Buttons;
        else
            curButtons = secondaryButtons;
        switch (key)
        {
            case ConsoleKey.T:
                RText = $"{Console.BufferWidth} X {Console.BufferHeight}";
                break;
            case ConsoleKey.UpArrow:
                curButtons.SubstractChoice(true);
                break;
            case ConsoleKey.DownArrow:
                curButtons.AddChoice(true);
                break;
            case ConsoleKey.X:
                if (secondaryButtons != null)
                    secondaryButtons = null;
                break;
            case ConsoleKey.Z:
                curButtons.ExecuteChoice();
                break;
            default:
                break;
        }
    }

    //Yeeeeeaaaaaah, I need to redo that one
    public void DrawUi()
    {
        string rika = rik.GetImageText();
        var lines = rika.Split('\n');
        string text = "";
        int wrdCount = Buttons.Count;
        List<string> rlines = StringToLines(rtext, width2 - 2 - 29, height - 4);
        int linecount = rlines.Count;
        for (int i = 0; i < height; i++)
        {
            string msg = "  ";
            string txt = "";
            string col = buttons.GetTextColor((i - 1) / 2);
            string scol = "";

            if ((i - 1) / 2 < wrdCount && i > 0 && i % 2 == 0)
            {
                msg += Buttons[(i - 1) / 2].Text;
            }
            if (secondaryButtons == null)
            {
                if ((i - 1) / 2 < linecount && i > 0 && i % 2 == 0)
                {
                    txt += rlines[(i - 1) / 2];
                }
            }
            else
            {
                if ((i - 1) / 2 < secondaryButtons.Count && i > 0 && i % 2 == 0)
                {
                    scol = secondaryButtons.GetTextColor((i - 1) / 2);
                    txt += secondaryButtons[(i - 1) / 2].Text;
                }
            }
            if (i == 0)
                text += "╔" + new string('═', 28) + "╦" + new string('═', width1 - 1) + "╦" + new string('═', width2 - 1 - 29) + "╗\n";
            else if (i == height - 1)
                text += "╚" + new string('═', 28) + "╩" + new string('═', width1 - 1) + "╩" + new string('═', width2 - 1 - 29) + "╝\n";
            else
            {
                string icoLine;
                if (i - 1 >= lines.Length)
                {
                    icoLine = new string(' ', rik.DrawWidth * 2);
                }
                else
                {
                    icoLine = lines[i - 1];
                }
                text += "║" + icoLine + "║" + col + msg.PadRight(width1 - 1) + "\e[0m║ " + scol + txt.PadRight(width2 - 2 - 29) + "\e[0m║\n";
            }
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

    public static void RunCommand(Panel? panel)
    {
        if (panel != null)
            panel.RText = "And so, you ran away...";
        GameManager.Instance.EndGame();
    }

    public static void FightCommand(Panel? panel)
    {
        if (panel != null)
        {
            if (GameManager.Instance.CurrentWave.Count != 0)
            {
                SoundManager.PlaySound("CursorMove");
                panel.SecondaryButtons = new ButtonCollection(panel);
                foreach (Enemy i in GameManager.Instance.CurrentWave)
                {
                    Button btn = new Button($"{i.Name}  ({i.Hp}/{i.MaxHp})", EnemyAttackCommand);
                    panel.SecondaryButtons.Add(btn);
                }
            }
            else
                SoundManager.PlaySound("Nuhuh");
        }
    }
    
    public static void EnemyAttackCommand(Panel? panel)
    {
        if (panel != null)
        {
            if (panel.SecondaryButtons!=null)
            {
                int ind = panel.SecondaryButtons.Choice;
                Wave wave = GameManager.Instance.CurrentWave;
                SoundManager.PlaySound("punch");
                int dmg = wave[ind].Hurt(GameManager.Instance.MainChar.Atk);
                panel.RText = $"You attacked {wave[ind].Name} for {dmg} HP! It didn't really like that!";
                panel.SecondaryButtons = null;
            }
        }
    }

    readonly IEnumerable<Button> DefaultButtons = new List<Button>(
        [
        new Button("FIGHT", FightCommand),
        new Button("SPELL"),
        new Button("INVENTORY"),
        new Button("DEFEND"),
        new Button("RUN", RunCommand)
        ]
    );
}