using System;
using System.Threading;
using System.Drawing;
using System.Media;
using ImageMagick;

class Program
{
    static void Main()
    {
        bool resizing = false;
        string image = @"./assets/WeirdArena.gif";
        string enemyImage = @"./assets/Ibiruai.gif";
        var enemyFromFile = new MagickImageCollection(enemyImage);
        var imageFromFile = new MagickImageCollection(image);
        int imageNum = imageFromFile.Count;
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
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.LeftArrow)
                fps -= 1;
                if (key == ConsoleKey.RightArrow)
                    fps += 1;
                }
            text = "";
            stage = new MagickImage(imageFromFile[0]);
            stage.Composite(enemyFromFile[(imageFrame/15)%(imageNum+1)],28,7+(int)(Math.Sin((MathF.PI/180)*imageFrame)*4), CompositeOperator.Over);
            var pixels = stage.GetPixels();
            uint imageWidth = stage.Width;
            var imageHeight = Math.Min(stage.Height, Console.BufferHeight);
            for (int y = 0; y < imageHeight; y++)
            {
                for (int x = 0; x < imageWidth; x++)
                {
                    var col = pixels[x, y].ToColor();
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
            text += $"{fps} Frames per Second | {Console.BufferWidth} width and {Console.BufferHeight} height";
            Console.Write(text);
            Console.ResetColor();
            imageFrame++;
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(1000/fps);
        }
    }
}