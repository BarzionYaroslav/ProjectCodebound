using System;
using System.Threading;
using System.Drawing;
using System.Media;
using ImageMagick;
using NetCoreAudio;

class Program
{
    static void Main()
    {
        const int nativeX = 180;
        const int nativeY = 50;
        bool gameStopped = false;
        string audioFile = @"./assets/ElectricHeritage.wav";
        Player audPlayer = new Player();
        audPlayer.SetVolume(100);
        string image = @"./assets/WeirdArena.gif";
        string enemyImage = @"./assets/punchy_bad.gif";
        var enemyFromFile = new MagickImageCollection(enemyImage);
        var imageFromFile = new MagickImageCollection(image);
        var arm1 = new MagickImageCollection(@"./assets/bad_arm1.gif");
        var arm2 = new MagickImageCollection(@"./assets/bad_arm2.gif");
        int imageNum = enemyFromFile.Count;
        int imageFrame = 0;
        int fps = 60;
        string text;
        int choice = 0;
        Console.Clear();
        Console.CursorVisible = false;
        MagickImage stage;
        List<uint> bufferSize = new List<uint>(
            [Convert.ToUInt16(Math.Min(Console.BufferWidth/2,nativeX/2)),
            Convert.ToUInt16(Math.Min(Console.BufferHeight-1,nativeY-1))]
            );
        uint[] bufferSizePrev = [0, 0];
        bufferSize.CopyTo(bufferSizePrev);
        (int x, int y) pos = (0, 0);
        while (!gameStopped)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.LeftArrow)
                    fps -= 1;
                if (key == ConsoleKey.RightArrow)
                    fps += 1;
                if (key == ConsoleKey.UpArrow)
                    choice -= 1;
                if (key == ConsoleKey.DownArrow)
                    choice += 1;
                if (key == ConsoleKey.R)
                {
                    Console.Clear();
                }
                if (key == ConsoleKey.Escape)
                {
                    gameStopped = true;
                    break;
                }
                if (key == ConsoleKey.P && !audPlayer.Playing)
                    audPlayer.Play(audioFile);
                if (key == ConsoleKey.S && audPlayer.Playing)
                    audPlayer.Stop();
            }
            text = "";
            bufferSize = new List<uint>(
            [Convert.ToUInt16(Math.Min(Console.BufferWidth/2,nativeX/2)),
            Convert.ToUInt16(Math.Min(Console.BufferHeight-1,nativeY-1))]
            );
            if (!Enumerable.SequenceEqual(bufferSize, bufferSizePrev))
                Console.Clear();
            bufferSize.CopyTo(bufferSizePrev);
            stage = new MagickImage(imageFromFile[0]);
            stage.Composite(enemyFromFile[(imageFrame / 5) % imageNum], 32, -6 + (int)(Math.Sin((MathF.PI / 180) * imageFrame * 4) * 3), CompositeOperator.Over);
            stage.Composite(arm1[0], -3 + (int)(Math.Cos((MathF.PI / 180) * imageFrame * 5) * 3), -3 + (int)(Math.Sin((MathF.PI / 180) * imageFrame * 5) * 2), CompositeOperator.Over);
            stage.Composite(arm2[0], (90 - 28) - (int)(Math.Cos((MathF.PI / 180) * imageFrame * 5) * 3), -3 + (int)(Math.Sin((MathF.PI / 180) * imageFrame * 5) * 2), CompositeOperator.Over);
            stage.Resize(bufferSize[0], bufferSize[1], FilterType.Point);
            var pixels = stage.GetPixels();
            var imageWidth = stage.Width;
            var imageHeight = stage.Height;
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
            double width1 = imageWidth * 2 / 4;
            double width2 = imageWidth * 2 * 3 / 4;
            DrawUi((int)Math.Floor(width1), (int)Math.Floor(width2), (int)bufferSize[1] - ((int)imageHeight + 1),
            [
                "FIGHT",
                "SPELL",
                "INVENTORY",
                "DEFEND",
                "RUN"
            ], choice, $"So, like, I can add some 🪶Feathers🪶, but apparently BIG TEXT is a bit out of my league suddenly, how the fuck is THAT fair-? Who made those Unicodes and ANSI break codes where big text doesn't even fucking work half the time with the different font sizes being impossible to edit in runtime???");
            pos = Console.GetCursorPosition();
            Console.ResetColor();
            imageFrame++;
            Thread.Sleep(1000 / fps);
        }
        Console.Clear();
        Console.SetCursorPosition(pos.x, pos.y);
    }

    static void DrawUi(int width1, int width2, int height, List<string> words, int choice, string rtext)
    {
        string text = "";
        int wrdCount = words.Count;
        List<string> rlines = StringToLines(rtext, width2 - 2, height - 4);
        int linecount = rlines.Count;
        for (int i = 0; i < height; i++)
        {
            string msg = "  ";
            string txt = "";
            string col = "\e[m";
            if ((i - 1) / 2 < wrdCount && i > 0 && i % 2 == 0)
            {
                msg += words[(i - 1) / 2];
            }
            if ((i - 1) / 2 < linecount && i > 0 && i % 2 == 0)
            {
                txt += rlines[(i - 1) / 2];
            }
            if (choice == (i - 1) / 2)
                col = "\e[38:5:220m";
            if (i == 0)
                text += "╔" + new string('═', width1 - 1) + "╦" + new string('═', width2 - 1) + "╗\n";
            else if (i == height - 1)
                text += "╚" + new string('═', width1 - 1) + "╩" + new string('═', width2 - 1) + "╝\n";
            else
                text += "║" + col + msg.PadRight(width1 - 1) + "\e[m║ " + txt.PadRight(width2 - 2) + "║\n";
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
}