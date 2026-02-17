using System;
using System.Threading;
using System.Drawing;
using ImageMagick;

class Program
{
    static void Main()
    {
        var imageFromFile = new MagickImageCollection(@"./assets/tenna-dancing-deltarune.gif");
        int imageNum = imageFromFile.Count;
        uint imageWidth = imageFromFile[0].Width;
        uint imageHeight = 32;
        int imageFrame = 0;
        int fps = 60;
        Console.Clear();
        Console.CursorVisible = false;
        while (true)
        {
            string text = "";
            var pixels = imageFromFile[imageFrame].GetPixels();
            for (int y=0; y<imageHeight;y++)
            {
                for (int x = 0; x < imageWidth; x++)
                {
                    var col = pixels[x, y].ToColor();
                    if (col == null)
                    {
                        text += " ";
                        break;
                    }
                    var arr = col.ToByteArray();
                    if (arr[3] == 255)
                        text += $"\e[38;2;{arr[0]};{arr[1]};{arr[2]}m██";
                    else
                        text += " ";
                }
                text += "\n";
            }
            Console.Write(text);
            imageFrame++;
            if (imageFrame > imageNum)
            {
                imageFrame = 0;
            }
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(1000/fps);
        }
    }
}