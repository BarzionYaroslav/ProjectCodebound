using System;
using System.Threading;
using Codebound.System.UI;
using ImageMagick;

namespace Codebound.System;

public delegate void KeyEventHandler(ConsoleKey key);
public static class Game
{
    public static int Fps
    {
        get { return fps; }
        set { fps = value; }
    }
    private static int fps = 60;
    public static bool GameStopped
    {
        get { return gameStopped; }
        set { gameStopped = value; }
    }
    private static bool gameStopped = false;
    static private List<uint> bufferSize = new List<uint>();
    static private uint[] bufferSizePrev = [0, 0];
    static private int imageFrame = 0;

    public static void GameLoop()
    {
        Console.Clear();
        Console.CursorVisible = false;
        bufferSize = new List<uint>(
            [Convert.ToUInt16(Math.Min(Console.BufferWidth/2,NativeX/2)),
            Convert.ToUInt16(Math.Min(Console.BufferHeight-1,NativeY-1))]
            );
        bufferSize.CopyTo(bufferSizePrev);
        while (!gameStopped)
        {
            Input();
            Update();
            CheckBufferSize();
            Render();
            Thread.Sleep(1000 / fps);
        }
    }

    public static void CheckBufferSize()
    {
        bufferSize = new List<uint>(
            [Convert.ToUInt16(Math.Min(Console.BufferWidth/2,NativeX/2)),
            Convert.ToUInt16(Math.Min(Console.BufferHeight-1,NativeY-1))]
            );
            if (!Enumerable.SequenceEqual(bufferSize, bufferSizePrev))
                Console.Clear();
            bufferSize.CopyTo(bufferSizePrev);
    }

    public static void Update()
    { }

    //This will live here for now, until I get better code done...
    public static void Render()
    {
        string image = @"./assets/WeirdArena.gif";
        string enemyImage = @"./assets/punchy_bad.gif";
        var enemyFromFile = new MagickImageCollection(enemyImage);
        var imageFromFile = new MagickImageCollection(image);
        var arm1 = new MagickImageCollection(@"./assets/bad_arm1.gif");
        var arm2 = new MagickImageCollection(@"./assets/bad_arm2.gif");
        int imageNum = enemyFromFile.Count;
        Console.SetCursorPosition(0, 0);
        var stage = new MagickImage(imageFromFile[0]);
        stage.Composite(enemyFromFile[(imageFrame / 5) % imageNum], 32, -6 + (int)(Math.Sin((MathF.PI / 180) * imageFrame * 4) * 3), CompositeOperator.Over);
        stage.Composite(arm1[0], -3 + (int)(Math.Cos((MathF.PI / 180) * imageFrame * 5) * 3), -3 + (int)(Math.Sin((MathF.PI / 180) * imageFrame * 5) * 2), CompositeOperator.Over);
        stage.Composite(arm2[0], (90 - 28) - (int)(Math.Cos((MathF.PI / 180) * imageFrame * 5) * 3), -3 + (int)(Math.Sin((MathF.PI / 180) * imageFrame * 5) * 2), CompositeOperator.Over);
        stage.Resize(bufferSize[0], bufferSize[1], FilterType.Point);
        var pixels = stage.GetPixels();
        var imageWidth = stage.Width;
        var imageHeight = stage.Height;
        var text = "";
        Console.SetCursorPosition(0, 0);
        for (int y = 0; y < imageHeight; y++)
        {
            for (int x = 0; x < imageWidth; x++)
            {
                var pix = pixels[x, y];
                if (pix == null)
                {
                    text += "  ";
                    break;
                }
                var col = pix.ToColor();
                if (col == null)
                {
                    text += "  ";
                    break;
                }
                var arr = col.ToByteArray();
                if (arr[3] > 0)
                    text += $"\e[38;2;{arr[0]};{arr[1]};{arr[2]}m██\e[0m";
                else
                    text += "  ";
            }
            text += "\n";
        }
        Console.Write(text);
        MainPanel.DrawUi();
        Console.ResetColor();
        imageFrame++;
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

    static readonly Panel MainPanel = new Panel(NativeX * 1 / 4, NativeX * 3 / 4, 16, "UNOWEN");
    const int NativeX = 180;
    const int NativeY = 50;
}