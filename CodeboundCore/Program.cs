using System;
using System.Threading;
using System.Drawing;
using System.Media;
using ImageMagick;
using NAudio.Wave;

class Program
{
    static void Main()
    {
        var audioFile = new AudioFileReader(@"./assets/15. Heian Alien.mp3");
        var outputDevice = new WaveOutEvent();
        outputDevice.Init(audioFile);
        bool resizing = false;
        string image = @"./assets/WeirdArena.gif";
        string enemyImage = @"./assets/punchy_bag.gif";
        var enemyFromFile = new MagickImageCollection(enemyImage);
        var imageFromFile = new MagickImageCollection(image);
        int imageNum = enemyFromFile.Count;
        List<uint> bufferSize = new List<uint>(
            [Convert.ToUInt16(Console.BufferWidth/2),
            Convert.ToUInt16(Console.BufferHeight-1)]
            );
        if (resizing)
        {
            foreach (var img in imageFromFile)
            {
                img.Resize(bufferSize[0], bufferSize[1], FilterType.Point);
            }
        }
        int imageFrame = 0;
        int fps = 60;
        string text;
        Console.Clear();
        Console.CursorVisible = false;
        MagickImage stage;
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.LeftArrow)
                    fps -= 1;
                if (key == ConsoleKey.RightArrow)
                    fps += 1;
                if (key == ConsoleKey.P && outputDevice.PlaybackState != PlaybackState.Playing)
                    outputDevice.Play();
                if (key == ConsoleKey.S && outputDevice.PlaybackState == PlaybackState.Playing)
                    outputDevice.Stop();
            }
            text = "";
            stage = new MagickImage(imageFromFile[0]);
            stage.Composite(enemyFromFile[(imageFrame / 5) % imageNum], 32, -6 + (int)Math.Sin((MathF.PI / 180) * imageFrame) * 2, CompositeOperator.Over);
            var pixels = stage.GetPixels();
            uint imageWidth = stage.Width;
            var imageHeight = Math.Min(stage.Height, Console.BufferHeight);
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
            text += $"\e[1m{fps} Frames per Second | {Console.BufferWidth} width and {Console.BufferHeight} height";
            text += $"\nIs music playing: {outputDevice.PlaybackState == PlaybackState.Playing}";
            Console.Write(text);
            Console.ResetColor();
            imageFrame++;
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(1000 / fps);
        }
    }
}