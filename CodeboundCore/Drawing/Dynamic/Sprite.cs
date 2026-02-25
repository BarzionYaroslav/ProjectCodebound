using ImageMagick;
using Codebound.System;
namespace Codebound.Drawing;
public class Sprite: IDrawableDynamic
{
    public MagickImageCollection Image {
        get { return image; }
        set
        {
            if (value != null)
                image = value;
            else
                throw new NullReferenceException();
        }
    }
    public float ImageIndex {
        get { return imageIndex; }
        set{ imageIndex = value % ImageCount; }
    }
    public float ImageSpeed {
        get { return imageSpeed; }
        set { imageSpeed = value; } 
    }
    public float ImageCount { get { return Image.Count; } }
    public int DrawHeight {
        get { return drawHeight; }
        set
        {
            if (value > 0)
                drawHeight = value;
        } 
    }
    public int DrawWidth {
        get { return drawWidth; }
        set
        {
            if (value > 0)
                drawWidth = value;
        } 
    }
    public int X { get { return x; } set { x = value; } }
    public int Y { get { return y; } set { y = value; } }
    public int Z { get { return z; } set { z = value; } }
    public int StartX { get { return startX; } set { startX = value; } }
    public int StartY { get { return startY; } set { startY = value; } }
    public int Depth {
        get { return depth; }
        set
        {
            if (value >= 0 && value <= Game.MaxDepth)
                depth = value;
        } 
    }
    public MagickImage Frame { get { return GetFrame(); } }
    public Sprite()
    {
        DrawHeight = (int)Image[0].Height;
        DrawWidth = (int)Image[0].Width;
        Game.UpdateStarted += UpdateValues;
        Game.RenderStarted += Draw;
    }
    public Sprite(string path, int x, int y, float imageSpeed, int depth)
    {
        if (File.Exists(path))
            Image = new MagickImageCollection(path);
        X = x;
        Y = y;
        ImageSpeed = imageSpeed;
        Depth = depth;
        StartX = X;
        StartY = Y;
        DrawHeight = (int)Image[0].Height;
        DrawWidth = (int)Image[0].Width;
        Game.UpdateStarted += UpdateValues;
        Game.RenderStarted += Draw;
    }
    public void Draw(StageImage stage, int depth)
    {
        if (depth == this.Depth)
        {
            var temp = stage.GetFrame();
            temp.Composite(GetFrame(), X, Y, CompositeOperator.Over);
        }
    }
    public void Dispose()
    {
        Game.UpdateStarted -= UpdateValues;
        Game.RenderStarted -= Draw;
    }
    public void UpdateValues()
    {
        ImageIndex += ImageSpeed;
    }
    public MagickImage GetFrame()
    {
        MagickImage answer = (MagickImage)Image[(int)ImageIndex];
        answer.Resize(
            (uint)(DrawWidth - Z),
            (uint)(DrawHeight - Z),
            FilterType.Point
            );
        answer.Colorize(
            new MagickColor(0, 0, 0, 255),
            new Percentage(Math.Max(0,Z * darkener))
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
            text += GetPixelText(x, num,pixels);
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
                text+=GetPixelText(x, y,pixels);
            }
            text += "\n";
        }
        return text;
    }

    public string GetPixelText(int x, int y,IPixelCollection<byte> pixels)
    {
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
            return $"\e[38;2;{arr[0]};{arr[1]};{arr[2]}m██\e[0m";
        return "  ";
    }
    private MagickImageCollection image = new MagickImageCollection(
            [
            new MagickImage(
                new MagickColor(255, 0, 0),
                5,
                5
            )
            ]
        );
    private float imageIndex = 0;
    private float imageSpeed = 0;
    private int drawHeight;
    private int drawWidth;
    private int x = 0;
    private int y = 0;
    private int z = 0;
    private int startX = 0;
    private int startY = 0;
    private int depth = 0;
    private readonly int darkener = 5;
}