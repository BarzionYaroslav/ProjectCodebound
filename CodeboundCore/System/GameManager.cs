using System.Diagnostics;
using Codebound.System.UI;
using Codebound.Drawing;
using Codebound.System.Randomness;
using Codebound.System.Control;

namespace Codebound.System;


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
                return RenderStarted.GetInvocationList().Count();
            return 0;
        }
    }
    public int UpdateAmount
    {
        get
        {
            if (UpdateStarted != null)
                return UpdateStarted.GetInvocationList().Count();
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
    public Panel MainPanel { get; private set; }
    private StageImage stage;
    public IRandomProvider Randomizer { get; private set; }
    public InputHandler inputHandler { get; private set; }

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
        inputHandler = new InputHandler();
        inputHandler.BindKey(ConsoleKey.R, new WaveRerollCommand());
        inputHandler.BindKey(ConsoleKey.Escape, new GameEndCommand());
        inputHandler.BindKey(ConsoleKey.DownArrow, new PanelDownCommand(MainPanel));
        inputHandler.BindKey(ConsoleKey.UpArrow, new PanelUpCommand(MainPanel));
        inputHandler.BindKey(ConsoleKey.Z, new PanelSelectCommand(MainPanel));
        inputHandler.BindKey(ConsoleKey.X, new PanelBackCommand(MainPanel));
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
        CheckBufferSize();
        if (bufferSize[0] < NativeX || bufferSize[1] < NativeY)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(bufferErrorMessage);
        }
        else
        {
            for (int i = MaxDepth; i >= 0; i--)
                RenderStarted?.Invoke(stage, i);
            var text = stage.GetImageText();
            Console.SetCursorPosition(0, 0);
            Console.Write(text);
            MainPanel.DrawUi();
            Console.ResetColor();
        }
    }

    private void Input()
    {
        inputHandler.HandleInput();
    }

    public void EndGame()
    {
        gameStopped = true;
    }
    static public event RenderEventHandler? RenderStarted;
    static public event UpdateEventHandler? UpdateStarted;
    static public event BufferChangeEventHandler? BufferChanged;

    private readonly string bufferErrorMessage;
}