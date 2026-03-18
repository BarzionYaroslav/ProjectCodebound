using System;
using System.Threading;
using System.Diagnostics;
using Codebound.System.UI;
using ImageMagick;
using NetCoreAudio;
using Codebound.Drawing;
using Codebound.Entities.Opponents;

namespace Codebound.System;


public delegate void KeyEventHandler(ConsoleKey key);
public delegate void UpdateEventHandler();
public delegate void RenderEventHandler(StageImage stage, int depth);
public delegate void BufferChangeEventHandler(int width, int height);
public class GameManager
{
    public int SpriteAmount
    {
        get
        {
            if (RenderStarted != null)
                return RenderStarted!.GetInvocationList().Count();
            return 0;
        }
    }
    public int Delta { get; private set; }
    public int QuitDelay { get; private set; }
    public int QuitChange { get; private set; }
    public int StageWidth { get; private set; }
    public int StageHeight { get; private set; }
    public int NativeX { get; private set; }
    public int NativeY { get; private set; }
    public int MaxDepth { get; private set; }
    public int Fps { get; private set; }
    static public int Siner
    {
        get { return siner; }
        private set { siner = value % 360; }
    }
    static private int siner;
    private bool gameStopped = false;
    private List<uint> bufferSize = new List<uint>();
    private uint[] bufferSizePrev = [0, 0];
    private static GameManager? _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }
    private Panel mainPanel;
    private Wave currentWave;
    private StageImage stage;
    private string prepText;
    private List<IEnemyFactory> prepFactories;

    private GameManager()
    {
        QuitDelay = 5;
        QuitChange = 10;
        Fps = 60;
        StageWidth = 90;
        StageHeight = 32;
        NativeX = 180;
        NativeY = 50;
        MaxDepth = 16;
        Siner = 0;
        stage = new StageImage((uint)StageWidth, (uint)StageHeight);
        currentWave = new Wave();
        mainPanel = new Panel(NativeX * 1 / 4, NativeX * 3 / 4, 16, "RIKA!!!");
        Random rand = new Random((int)DateTime.Now.Ticks);
        var checker = rand.Next(15);
        switch (checker)
        {
            case 0:
                prepText = "THEY OPENED THE GAME!!! RATTLE 'EM BOYS!!!";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new SkulatraFactory(0,-2,0),
                    new SkulatraFactory(30,-1,0),
                    new SkulatraFactory(60,-2,0)
                    ]
                );
                break;
            case 1:
                switch (rand.Next(5))
                {
                    case 0:
                        prepText = "Punchy Bad accidentally blocked your path. You intentionally started the fight!";
                        break;
                    case 1:
                        prepText = "Punchy Bad fills his Handbads with horseshoes.";
                        break;
                    case 2:
                        prepText = "Punchy Bad contemplates his name for a moment. Bad Punch blocks your path!";
                        break;
                    case 3:
                        prepText = "Punchy Bad considers sandbagging, but remembers that he isn't a Desert Boss.";
                        break;
                    case 4:
                        prepText = "Punchy Bad wonders when Yaroslav will finish the screen resize code. He easily gets distracted with a fight!";
                        break;
                    default:
                        prepText = "Punchy Bad swings in like a wrecking ball!";
                        break;
                }
                prepFactories = new List<IEnemyFactory>(
                    [
                    new PunchyBadHandFactory(0,-3,0),
                    new PunchyBadFactory(32,-6,1),
                    new PunchyBadHandFactory(90-32,-3,0)
                    ]
                );
                break;
            case 3:
                prepText = "I spy with my floating eye something ending with this fight.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new IbiruaiFactory(30,10,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 4:
                prepText = "It stares at you through the stitches.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new BlaindaiFactory(30,7,0)
                    ]
                );
                break;
            case 5:
                prepText = "Amblyopia.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,1),
                    new BlaindaiFactory(30,7,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 6:
                prepText = "Sleep eternal quiescent dreams.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new BlaindaiFactory(2,6,1),
                    new BlaindaiFactory(30,7,0),
                    new BlaindaiFactory(58,6,1)
                    ]
                );
                break;
            case 7:
                prepText = "Uneasy alliance.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new SkulatraFactory(0,-2,1),
                    new BlaindaiFactory(30,7,0),
                    new SkulatraFactory(60,-2,1)
                    ]
                );
                break;
            case 8:
                prepText = "Not again...";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new SkulatraFactory(0,-2,1),
                    new IbiruaiFactory(30,7,0),
                    new SkulatraFactory(60,-2,1)
                    ]
                );
                break;
            case 9:
                prepText = "Ibiruai would take revenge, if only context for it was provided.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new SkulatraFactory(30,-1,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 10:
                prepText = "THEY OPENED THE GAME!!! RATTLE 'EM... boys...?";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new BlaindaiFactory(2,6,2),
                    new SkulatraFactory(30,-1,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 11:
                prepText = "Yokanten slides in!";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new YokantenFactory(37,15,0)
                    ]
                );
                break;
            case 12:
                prepText = "Early Game Enemy convention.";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new YokantenFactory(37,15,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            case 13:
                prepText = "Maybe the true enemies were the friends we made along the way...";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiderFactory(30,-6,0)
                    ]
                );
                break;
            case 14:
                prepText = "Kneel before the Great Ibiruai Tamer! Let his matcha colors be known across the lands!";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new IbiruaiFactory(2,6,2),
                    new IbiruaiderFactory(30,-6,0),
                    new IbiruaiFactory(58,6,2)
                    ]
                );
                break;
            default:
                prepText = "Punchy Bag swings in like a fluff-filled pinata!";
                prepFactories = new List<IEnemyFactory>(
                    [
                    new PunchyBagFactory((90-20)/2,-6,0)
                    ]
                );
                break;
        }
    }

    public void GameLoop()
    {
        Console.Clear();
        Console.CursorVisible = false;
        bufferSize = new List<uint>(
            [Convert.ToUInt16(Math.Min(Console.BufferWidth,NativeX)),
            Convert.ToUInt16(Math.Min(Console.BufferHeight,NativeY))]
            );
        bufferSize.CopyTo(bufferSizePrev);
        Delta = 0;
        PrepareFight(prepText, prepFactories);
        while (!gameStopped)
        {
            var watch = Stopwatch.StartNew();
            Input();
            Update();
            CheckBufferSize();
            //Bandaid solution for the time being. I'll figure it out later
            if (bufferSize[0] < NativeX || bufferSize[1] < NativeY)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write($"Due to the resize code being unfinished, it's impossible to render the game at the buffer size less than {NativeX} X {NativeY}\n");
                Console.Write("Please make sure it's at least that by editing the console settings!\n");
                Console.Write($"Current Buffer Size: {bufferSize[0]} X {bufferSize[1]}");
            }
            else
                Render();
            watch.Stop();
            var timeTaken = (int)watch.ElapsedMilliseconds;
            int waitTime = (1000 / Fps) - timeTaken;
            if (waitTime < 0)
                waitTime = 0;
            Thread.Sleep(waitTime);
            Delta = timeTaken;
        }
        while (stage.Alpha > 0)
        {
            SoundManager.Kill();
            var watch = Stopwatch.StartNew();
            stage.Alpha -= QuitChange;
            CheckBufferSize();
            //Bandaid solution for the time being. I'll figure it out later
            if (!(bufferSize[0] < NativeX || bufferSize[1] < NativeY))
                Render();
            watch.Stop();
            var timeTaken = (int)watch.ElapsedMilliseconds;
            int waitTime = QuitDelay - timeTaken;
            if (waitTime < 0)
                waitTime = 0;
            Thread.Sleep(waitTime);
        }
    }

    private void PrepareFight(string text, List<IEnemyFactory> factories)
    {
        mainPanel.RText = text;
        foreach (IEnemyFactory i in factories)
        {
            i.Create();
        }
    }

    private void CheckBufferSize()
    {
        bufferSize = new List<uint>(
            [Convert.ToUInt16(Math.Min(Console.BufferWidth,NativeX)),
            Convert.ToUInt16(Math.Min(Console.BufferHeight,NativeY))]
            );
        if (!Enumerable.SequenceEqual(bufferSize, bufferSizePrev))
            Console.Clear();
        bufferSize.CopyTo(bufferSizePrev);
        BufferChanged?.Invoke((int)bufferSize[0], (int)bufferSize[1]);
    }

    private void Update()
    {
        UpdateStarted?.Invoke();
        Siner++;
    }

    private void Render()
    {

        for (int i = MaxDepth; i >= 0; i--)
            RenderStarted?.Invoke(stage, i);
        var text = stage.GetImageText();
        Console.SetCursorPosition(0, 0);
        Console.Write(text);
        mainPanel.DrawUi();
        Console.ResetColor();
    }

    private void Input()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            KeyPressed?.Invoke(key);
            if (key == ConsoleKey.Escape)
            {
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        gameStopped = true;
    }

    static public event KeyEventHandler? KeyPressed;
    static public event RenderEventHandler? RenderStarted;
    static public event UpdateEventHandler? UpdateStarted;
    static public event BufferChangeEventHandler? BufferChanged;
}