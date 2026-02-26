using ImageMagick;
using Codebound.System;
namespace Codebound.Drawing;

public class StageImage: IDrawable
{
    public MagickImageCollection Image
    {
        get { return image; }
        set
        {
            if (value != null)
                image = value;
            else
                throw new NullReferenceException();
        }
    }
    public float ImageIndex
    {
        get { return imageIndex; }
        set { imageIndex = value % ImageCount; }
    }
    public float ImageSpeed
    {
        get { return imageSpeed; }
        set { imageSpeed = value; }
    }
    public float ImageCount { get { return Image.Count; } }
    public int DrawHeight
    {
        get { return drawHeight; }
        set
        {
            if (value > 0)
                drawHeight = value;
        }
    }
    public int DrawWidth
    {
        get { return drawWidth; }
        set
        {
            if (value > 0)
                drawWidth = value;
        }
    }
    public MagickImage Frame { get { return GetFrame(); } }
    public int Alpha {
        get { return alpha; }
        set
        {
            alpha = Math.Clamp(value, 0, 255);
        }
        }

    public StageImage()
    {
        DrawHeight = (int)Image[0].Height;
        DrawWidth = (int)Image[0].Width;
        Game.UpdateStarted += UpdateValues;
        Game.BufferChanged += Resizer;
    }

    public StageImage(uint width, uint height)
    {
        Image = new MagickImageCollection(
            [
            new MagickImage(
                new MagickColor(0, 0, 0, 0),
                width,
                height
            )
            ]
        );
        DrawHeight = (int)Image[0].Height;
        DrawWidth = (int)Image[0].Width;
        Game.UpdateStarted += UpdateValues;
        Game.BufferChanged += Resizer;
    }

    public StageImage(string path, float imageSpeed)
    {
        if (File.Exists(path))
            Image = new MagickImageCollection(path);
        ImageSpeed = imageSpeed;
        DrawHeight = (int)Image[0].Height;
        DrawWidth = (int)Image[0].Width;
        Game.UpdateStarted += UpdateValues;
        Game.BufferChanged += Resizer;
    }

    public StageImage(MagickImage copier)
    {
        Image = new MagickImageCollection([copier]);
        ImageSpeed = 0;
        DrawHeight = (int)Image[0].Height;
        DrawWidth = (int)Image[0].Width;
        Game.UpdateStarted += UpdateValues;
        Game.BufferChanged += Resizer;
    }

    public void Dispose()
    {
        Game.UpdateStarted -= UpdateValues;
        Game.BufferChanged -= Resizer;
    }

    public void UpdateValues()
    {
        ImageIndex += ImageSpeed;
    }

    public void Resizer(int width, int height)
    {
        DrawHeight = height;
        DrawWidth = width/2;
    }

    public MagickImage GetFrame()
    {
        MagickImage answer = (MagickImage)Image[(int)ImageIndex];
        answer.Resize(
            (uint)DrawWidth,
            (uint)DrawHeight,
            FilterType.Point
            );
        return answer;
    }

    public List<string> GetLines()
    {
        List<string> answer = new List<string>();
        for (int num = 0; num < this.Frame.Height; num++)
            answer.Add(GetLine(num));
        return answer;
    }

    public string GetLine(int num)
    {
        uint imageWidth = this.Frame.Width;
        uint imageHeight = this.Frame.Height;
        if (num > imageHeight)
            return "  ";
        string text = "";
        IPixelCollection<byte> pixels = this.Frame.GetPixels();
        for (int x = 0; x < imageWidth; x++)
            text += GetPixelText(x, num, pixels);
        return text;
    }

    public string GetImageText()
    {
        uint imageWidth = this.Frame.Width;
        uint imageHeight = this.Frame.Height;
        string text = "";
        IPixelCollection<byte> pixels = this.Frame.GetPixels();
        for (int y = 0; y < imageHeight; y++)
        {
            for (int x = 0; x < imageWidth; x++)
            {
                text+=GetPixelText(x, y, pixels);
            }
            text += "\n";
        }
        return text;
    }

    public string GetPixelText(int x, int y, IPixelCollection<byte> pixels)
    {
        string symbols;
        if (Alpha >= 255 * 4 / 5)
            symbols = "██";
        else if (Alpha >= 255 * 3 / 5)
            symbols = "▓▓";
        else if (Alpha >= 255 * 2 / 5)
            symbols = "▒▒";
        else if (Alpha >= 255 * 1 / 5)
            symbols = "░░";
        else
            return "  ";
        var pix = pixels[x, y];
        if (pix == null)
        {
            return "  ";
        }
        var col = pix.ToColor();
        if (col == null)
        {
            return "  ";
        }
        var arr = col.ToByteArray();
        if (arr[3] > 0)
            return $"\e[38;2;{arr[0]};{arr[1]};{arr[2]}m{symbols}\e[0m";
        return "  ";
    }

    private MagickImageCollection image = new MagickImageCollection(
            [
            new MagickImage(
                new MagickColor(255, 0, 0, 255),
                5,
                5
            )
            ]
        );
    private float imageIndex = 0;
    private float imageSpeed = 0;
    private int drawHeight;
    private int drawWidth;
    public int alpha = 255;
}