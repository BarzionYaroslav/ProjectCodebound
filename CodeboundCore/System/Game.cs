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
public static class Game
{
    static public Panel MainPanel
    {
        get { return mainPanel; }
        set
        {
            if (value != null)
                mainPanel = value;
            else
                throw new NullReferenceException();
        }
    }
    static public int Siner
    {
        get { return siner; }
        set { siner = value % 360; }
    }
    static public StageImage Stage
    {
        get { return stage; }
    }
    static public int Fps
    {
        get { return fps; }
        set { fps = value; }
    }
    static public bool GameStopped
    {
        get { return gameStopped; }
        set { gameStopped = value; }
    }
    static public List<uint> BufferSize => bufferSize;
    static private bool gameStopped = false;
    static private List<uint> bufferSize = new List<uint>();
    static private uint[] bufferSizePrev = [0, 0];
    static private int fps = DefaultFps;
    static private int siner = 0;
    static private Panel mainPanel = new Panel(NativeX * 1 / 4, NativeX * 3 / 4, 16, "RIKA!!!");
    static private Wave currentWave = new Wave();
    static private StageImage stage = new StageImage(
        StageWidth,
        StageHeight
    );

    public static void GameLoop()
    {
        Console.Clear();
        Console.CursorVisible = false;
        bufferSize = new List<uint>(
            [Convert.ToUInt16(Math.Min(Console.BufferWidth,NativeX)),
            Convert.ToUInt16(Math.Min(Console.BufferHeight-1,NativeY-1))]
            );
        bufferSize.CopyTo(bufferSizePrev);
        while (!gameStopped)
        {
            var watch = Stopwatch.StartNew();
            Input();
            Update();
            CheckBufferSize();
            Render();
            watch.Stop();
            var timeTaken = (int)watch.ElapsedMilliseconds;
            int waitTime = (1000 / fps) - timeTaken;
            if (waitTime < 0)
                waitTime = 0;
            Thread.Sleep(waitTime);
        }
        while (Stage.Alpha>0)
        {
            var watch = Stopwatch.StartNew();
            Stage.Alpha -= QuitChange;
            Render();
            watch.Stop();
            var timeTaken = (int)watch.ElapsedMilliseconds;
            int waitTime = QuitDelay - timeTaken;
            if (waitTime < 0)
                waitTime = 0;
            Thread.Sleep(waitTime);
        }
    }

    public static void CheckBufferSize()
    {
        bufferSize = new List<uint>(
            [Convert.ToUInt16(Math.Min(Console.BufferWidth,NativeX)),
            Convert.ToUInt16(Math.Min(Console.BufferHeight-1,NativeY-1))]
            );
            if (!Enumerable.SequenceEqual(bufferSize, bufferSizePrev))
                Console.Clear();
        bufferSize.CopyTo(bufferSizePrev);
        BufferChanged?.Invoke((int)bufferSize[0], (int)bufferSize[1]);
    }

    public static void Update()
    {
        UpdateStarted?.Invoke();
        Siner++;
    }

    public static void Render()
    {

        for (int i = MaxDepth; i >= 0; i--)
            RenderStarted?.Invoke(stage, i);
        var text = stage.GetImageText();
        Console.SetCursorPosition(0, 0);
        Console.Write(text);
        MainPanel.DrawUi();
        Console.ResetColor();
    }

    public static void Input()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            KeyPressed?.Invoke(key);
            if (key == ConsoleKey.Escape)
            {
                gameStopped = true;
            }
        }
    }

    public static event KeyEventHandler? KeyPressed;
    public static event RenderEventHandler? RenderStarted;
    public static event UpdateEventHandler? UpdateStarted;
    public static event BufferChangeEventHandler? BufferChanged;

    public const int QuitDelay = 5;
    public const int QuitChange = 10;
    public const int DefaultFps = 60;
    public const int StageWidth = 90;
    public const int StageHeight = 32;
    public const int NativeX = 180;
    public const int NativeY = 50;
    public const int MaxDepth = 16;
}