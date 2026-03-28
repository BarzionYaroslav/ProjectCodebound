using ImageMagick;
namespace Codebound.Drawing;

public interface IDrawable
{
    MagickImageCollection Image { get; }
    float ImageIndex { get; }
    float ImageSpeed { get; }
    float ImageCount { get { return Image.Count; } }
    int DrawHeight { get; }
    int DrawWidth { get; }
    MagickImage Frame { get { return GetFrame(); } }
    MagickImage GetFrame();
    void UpdateValues();
    List<string> GetLines();
    string GetLine(int num,IPixelCollection<byte> pixels);
    string GetImageText();
    string GetPixelText(int x, int y, IPixelCollection<byte> pixels);
}