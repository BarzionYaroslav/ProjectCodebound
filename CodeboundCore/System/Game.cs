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
        while (Stage.Alpha!=0)
        {
            var watch = Stopwatch.StartNew();
            Stage.Alpha -= 3;
            Render();
            watch.Stop();
            var timeTaken = (int)watch.ElapsedMilliseconds;
            int waitTime = (1000 / fps) - timeTaken;
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

    //This will live here for now, until I get better code done...
    public static void Render()
    {

        for (int i = MaxDepth; i >= 0; i--)
            RenderStarted?.Invoke(stage, i);
        // string image = @"./assets/WeirdArena.gif";
        // string enemyImage = @"./assets/punchy_bad.gif";
        // var enemyFromFile = new MagickImageCollection(enemyImage);
        // var imageFromFile = new MagickImageCollection(image);
        // var arm1 = new MagickImageCollection(@"./assets/bad_arm1.gif");
        // var arm2 = new MagickImageCollection(@"./assets/bad_arm2.gif");
        // int imageNum = enemyFromFile.Count;
        // Console.SetCursorPosition(0, 0);
        // stage.Composite(imageFromFile[0],0,0,CompositeOperator.Over);
        // stage.Composite(enemyFromFile[(imageFrame / 4) % imageNum], 32, -6 + (int)(Math.Sin((MathF.PI / 180) * imageFrame * 4) * 3), CompositeOperator.Over);
        // int z = (int)(Math.Sin((MathF.PI / 180) * imageFrame * 5) * 4);
        // arm1[0].Resize((uint)(arm1[0].Width - z), (uint)(arm1[0].Height - z), FilterType.Point);
        // arm1[0].Colorize(new MagickColor(0, 0, 0, 0),new Percentage((z+4)*5));
        // stage.Composite(arm1[0], (int)z - 3 + (int)(Math.Cos((MathF.PI / 180) * imageFrame * 5) * 3), -3 + (int)(Math.Sin((MathF.PI / 180) * imageFrame * 5) * 2), CompositeOperator.Over);
        // z = (int)(Math.Sin(180+(MathF.PI / 180) * imageFrame * 5) * 4);
        // arm2[0].Resize((uint)(arm2[0].Width - z), (uint)(arm2[0].Height - z), FilterType.Point);
        // arm2[0].Colorize(new MagickColor(0, 0, 0, 0),new Percentage((z+4)*5));
        // stage.Composite(arm2[0], (int)z+(90 - 28) - (int)(Math.Cos((MathF.PI / 180) * imageFrame * 5) * 3), -3 + (int)(Math.Sin((MathF.PI / 180) * imageFrame * 5) * 2), CompositeOperator.Over);
        // stage.Resize(bufferSize[0], bufferSize[1], FilterType.Point);
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


    public const int DefaultFps = 60;
    public const int StageWidth = 90;
    public const int StageHeight = 32;
    public const int NativeX = 180;
    public const int NativeY = 50;
    public const int MaxDepth = 16;
}