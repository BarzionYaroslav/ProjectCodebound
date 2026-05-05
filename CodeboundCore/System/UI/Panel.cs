using ImageMagick;
using Codebound.Drawing;
using Codebound.Entities.Opponents;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Codebound.Entities;
using Codebound.Items.Weapons;
namespace Codebound.System.UI;

public class Panel
{
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
    public ButtonCollection SecondaryButtons {
        get { return secondaryButtons; }
        set
        {
            if (value != null)
            {
                secondaryButtons = value;
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

    public int Width
    {
        get { return width; }
        set
        {
            if (value > 0)
            {
                width = value;
            }
        }
    }

    private ButtonCollection buttons = new ButtonCollection();
    private ButtonCollection secondaryButtons = new ButtonCollection();
    private string rtext = "UNOWEN";
    private int height;
    private int width;
    private IPanelState _state;
    public bool Active = true;

    public Panel(int width, int height, string rtext)
    {
        Width = width;
        Height = height;
        RText = rtext;
        _state = new PanelStateBattleMain(this);
        foreach (var i in DefaultButtons)
        {
            buttons.Add(i);
        }
        buttons.Link = this;
        GameManager.KeyPressed += HandleControls;
    }

    public void SetState(IPanelState state)
    {
        _state = state;
        _state.SetContext(this);
    }

    public void HandleControls(ConsoleKey key)
    {
        if (Active)
        {
            _state.HandleControls(key);
        }
    }

    public string MakeFloor(int width)
    {
        if (width < 0)
            width = 0;
        return new string(floorSymbol, width);
    }

    public string MakePartEdges(int width, bool top = false, PanelContinueOptions option = PanelContinueOptions.None)
    {
        if (width < 0)
            width = 0;
        string symbolSet;
        if (top)
        {
            symbolSet = topSymbols;
        }
        else
            symbolSet = bottomSymbols;
        char cornL;
        char cornR;
        switch (option)
        {
            case PanelContinueOptions.None:
                cornL = symbolSet[0];
                cornR = symbolSet[2];
                break;
            case PanelContinueOptions.Left:
                cornL = symbolSet[1];
                cornR = symbolSet[2];
                break;
            case PanelContinueOptions.Right:
                cornL = symbolSet[0];
                cornR = symbolSet[1];
                break;
            case PanelContinueOptions.Both:
                cornL = symbolSet[1];
                cornR = symbolSet[1];
                break;
            default:
                cornL = symbolSet[0];
                cornR = symbolSet[2];
                break;
        }
        string mid = MakeFloor(width);
        string answer = $"\e[0m{cornL}{mid}{cornR}";
        return answer;
    }

    public string MakePanelPart(int i, int width, int height, ButtonCollection content, PanelContinueOptions option = PanelContinueOptions.None)
    {
        if (width < 0)
            width = 0;
        string text;
        if (i == 0)
            text = MakePartEdges(width, true, option);
        else if (i == height - 1)
            text = MakePartEdges(width, false, option);
        else
        {
            string mid = "  ";
            string col = content.GetTextColor((i - 1) / 2);
            if ((i - 1) / 2 < content.Count && i > 0 && i % 2 == 0)
            {
                mid += content[(i - 1) / 2].Text;
            }
            text = wallSymbol + col + mid.PadRight(width) + "\e[0m" + wallSymbol;
        }
        if (option != PanelContinueOptions.Both && option != PanelContinueOptions.Right)
            text += "\n";
        return text;
    }

    public string MakePanelPart(int i, int width, int height, List<string> content, PanelContinueOptions option = PanelContinueOptions.None, bool gap = true)
    {
        if (width < 0)
            width = 0;
        string text;

        if (i == 0)
            text = MakePartEdges(width, true, option);
        else if (i == height - 1)
            text = MakePartEdges(width, false, option);
        else
        {
            string mid;
            if (gap)
            {
                mid = "  ";
                if ((i - 1) / 2 < content.Count && i > 0 && i % 2 == 0)
                {
                    mid += content[(i - 1) / 2];
                }
            }
            else
            {
                mid = "";
                if ((i - 1) < content.Count && i > 0)
                {
                    mid += content[i - 1];
                }
            }
            text = wallSymbol + mid.PadRight(width) + "\e[0m" + wallSymbol;
        }
        if (option != PanelContinueOptions.Both && option != PanelContinueOptions.Right)
            text += "\n";
        return text;
    }

    //Yeeeeeaaaaaah, I need to redo that one
    public void DrawUi()
    {
        _state.DrawUi();
    }

    public static List<string> StringToLines(string text, int size, int maxLines)
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

    public static List<string> StringToLines(List<string> text, int size, int maxLines)
    {
        List<string> answer = new List<string>();
        int counter = 0;
        foreach (var i in text)
        {
            var tempAns = Panel.StringToLines(i, size, maxLines);
            foreach (var n in tempAns)
            {
                answer.Add(n);
                counter += 1;
                if (counter >= maxLines)
                    return answer;
            }
        }
        return answer;
    }


    readonly IEnumerable<Button> DefaultButtons = new List<Button>(
        [
        new Button("FIGHT", new FightButtonStrategy()),
        new Button("SPELL"),
        new Button("INVENTORY", new InventoryButtonStrategy()),
        new Button("DEFEND"),
        new Button("RUN", new RunButtonStrategy())
        ]
    );
    public readonly List<BaseWeapon?> testList = new List<BaseWeapon?>(
        [
            null,
            new DamageDecorator(new Sword("Strong Sword", "A decorated sword!", 5),2),
            new DamageDecorator(new Sword("Weaker Sword", "A decorated sword, but worse!", 5),-2),
            new HealthDecorator(new Sword("Double Sword", "Literally double-edged sword!", 5),-2),
            new HealthDecorator(new DamageDecorator(new Sword("Thorn Sword", "Eye for an Eye", 5),10),-10),
            new HealthDecorator(new HealthDecorator(new DamageDecorator(new Sword("Health Check", "Extreme eye for an eye", 5),99),-20),-10),
        ]
    );

    private readonly string topSymbols = "╔╦╗";
    private readonly char wallSymbol = '║';
    private readonly char floorSymbol = '═';
    private readonly string bottomSymbols = "╚╩╝";
}