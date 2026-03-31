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
    private ButtonCollection? secondaryButtons;
    private string rtext = "UNOWEN";
    private int height;
    private int width;
    public int state = 0;

    public Panel(int width, int height, string rtext)
    {
        Width = width;
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

    public string MakeFloor(int width)
    {
        return new string(floorSymbol, width);
    }

    public string MakePartEdges(int width, bool top = false, PanelContinueOptions option = PanelContinueOptions.None)
    {
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
        var rik = GameManager.Instance.MainChar.Face;
        string text = "";
        List<string> playerDat = [.. GameManager.Instance.MainChar.ToString().Split('\n')];
        int datMax = 0;
        foreach (var i in playerDat)
        {
            if (i.Length>datMax)
            {
                datMax = i.Length;
            }
        }
        string icoText = rik.GetImageText();
        List<string> icolines = [.. icoText.Split('\n')];
        int lenCounter = 0;
        lenCounter += datMax + 4 + 2;
        lenCounter += rik.DrawWidth * 2 + 2;
        lenCounter += 12 + 2;
        var rlines = StringToLines(RText, width - lenCounter, height - 4);
        string enText;
        List<string> enlines = new List<string>();
        List<string> enmDat = new List<string>();
        int enmMax = 0;
        List<string> wtext = new List<string>();
        if (secondaryButtons!=null)
        {
            if (state == 1)
            {
                enText = GameManager.Instance.CurrentWave[secondaryButtons.Choice].Face.GetImageText();
                enlines = [.. enText.Split('\n')];
                enmDat = [.. GameManager.Instance.CurrentWave[secondaryButtons.Choice].ToString().Split('\n')];
                foreach (var i in enmDat)
                {
                    if (i.Length > enmMax)
                    {
                        enmMax = i.Length;
                    }
                }
            }
            if (state == 2)
            {
                var wep = testList[secondaryButtons.Choice];
                if (wep != null)
                    wtext = [.. wep.GetDescription().Split('\n')];
                else
                    wtext = new List<string>(["Unequip your weapon"]);
                enmMax = 82;
                wtext = StringToLines(wtext, enmMax - 6, height - 2);
            }
        }
        
        for (int i = 0; i < height; i++)
        {
            text += MakePanelPart(i, rik.DrawWidth * 2, height, icolines, PanelContinueOptions.Right, false);
            
            text += MakePanelPart(i, datMax + 4, height, playerDat, PanelContinueOptions.Both);
            
            text += MakePanelPart(i, 12, height, Buttons, PanelContinueOptions.Both);

            if (secondaryButtons == null)
            {
                text += MakePanelPart(i, width - lenCounter, height, rlines, PanelContinueOptions.Left);
            }
            else
            {
                if (state == 1)
                {
                    text += MakePanelPart(i, width - lenCounter - (rik.DrawWidth * 2) - 2 - (enmMax + 4) - 2, height, secondaryButtons, PanelContinueOptions.Both);
                    text += MakePanelPart(i, enmMax + 4, height, enmDat, PanelContinueOptions.Both);
                    text += MakePanelPart(i, rik.DrawWidth * 2, height, enlines, PanelContinueOptions.Left, false);
                }
                if (state==2)
                {
                    text += MakePanelPart(i, width - lenCounter - (enmMax + 4) - 2, height, secondaryButtons, PanelContinueOptions.Both);
                    text += MakePanelPart(i, enmMax + 4, height, wtext, PanelContinueOptions.Left);
                }
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

    static List<string> StringToLines(List<string> text, int size, int maxLines)
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
                    Button btn = new Button($"{i.Name}", EnemyAttackCommand);
                    panel.SecondaryButtons.Add(btn);
                }
                panel.state = 1;
            }
            else
                SoundManager.PlaySound("Nuhuh");
        }
    }
    public static void InventoryCommand(Panel? panel)
    {
        if (panel != null)
        {
            if (panel.testList.Count != 0)
            {
                SoundManager.PlaySound("CursorMove");
                panel.SecondaryButtons = new ButtonCollection(panel);
                foreach (var i in panel.testList)
                {
                    Button btn;
                    if (i == null)
                        btn = new Button($"Unequip", WeaponCommand);
                    else
                        btn = new Button($"{i.GetName()}", WeaponCommand);
                    panel.SecondaryButtons.Add(btn);
                }
                panel.state = 2;
            }
            else
                SoundManager.PlaySound("Nuhuh");
        }
    }

    public static void WeaponCommand(Panel? panel)
    {
        if (panel != null)
        {
            if (panel.SecondaryButtons != null)
            {
                int ind = panel.SecondaryButtons.Choice;
                Hero character = GameManager.Instance.MainChar;
                character.Weapon = panel.testList[ind];
                panel.SecondaryButtons = null;
                panel.state = 0;
            }
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
                Enemy enm = wave[ind];
                Hero character = GameManager.Instance.MainChar;
                if (character.Weapon != null)
                    character.Weapon.Use(character, enm);
                else
                {
                    SoundManager.PlaySound("punch");
                    new SpriteBuilder().SetSprite("punch_fx")
                        .SetImageSpeed(1f)
                        .SetPosition(
                            enm.Body[Enemy.BodyName].X + (enm.Body[Enemy.BodyName].DrawWidth/2) - 8,
                            enm.Body[Enemy.BodyName].Y + (enm.Body[Enemy.BodyName].DrawHeight/2) - 8
                            )
                        .SetDepth(0)
                        .SetVanish(true)
                        .Build();
                }
                int dmg = enm.Hurt(character.Atk);
                panel.RText = $"You attacked {enm.Name} for {dmg} HP! It didn't really like that!";
                panel.SecondaryButtons = null;
                panel.state = 0;
            }
        }
    }

    readonly IEnumerable<Button> DefaultButtons = new List<Button>(
        [
        new Button("FIGHT", FightCommand),
        new Button("SPELL"),
        new Button("INVENTORY", InventoryCommand),
        new Button("DEFEND"),
        new Button("RUN", RunCommand)
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