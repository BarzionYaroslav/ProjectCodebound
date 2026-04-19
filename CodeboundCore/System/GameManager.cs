using System;
using System.Threading;
using System.Diagnostics;
using Codebound.System.UI;
using Codebound.Drawing;
using Codebound.Entities.Opponents;
using Codebound.Entities;

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
    private List<int> bufferSize = new List<int>([0,0]);
    private List<int> bufferSizePrev = new List<int>([0,0]);
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
    public Panel MainPanel;
    private StageImage stage;
    public IRandomProvider Randomizer;

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
        bufferErrorMessage =
            "Due to the resize code being unfinished, it's " +
            $"impossible to render the game at the buffer size less than {NativeX} X {NativeY}\n" +
            "Please make sure it's at least that by editing the console settings!\n";
        stage = new StageImage((uint)StageWidth, (uint)StageHeight);
        MainPanel = new Panel(NativeX, 16, "RIKA!!!");
        Randomizer = new RandomAdapter();
    }

    public void GameLoop()
    {
        Console.Clear();
        Console.CursorVisible = false;
        CheckBufferSize();
        Delta = 0;
        BattleManager.Instance.PrepareFight();
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
                Console.Write(bufferErrorMessage);
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

    private void CheckBufferSize()
    {
        bufferSize[0] = Math.Min(Console.BufferWidth, NativeX);
        bufferSize[1] = Math.Min(Console.BufferHeight,NativeY);
        if (!Enumerable.SequenceEqual(bufferSize, bufferSizePrev))
            Console.Clear();
        bufferSizePrev[0] = bufferSize[0];
        bufferSizePrev[1] = bufferSize[1];
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
        MainPanel.DrawUi();
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

    private readonly string bufferErrorMessage;
}