using ImageMagick;
namespace Codebound.Drawing;

public interface IDrawable
{
    MagickImageCollection Image { get; set; }
    float ImageIndex { get; set; }
    float ImageSpeed { get; set; }
    float ImageCount { get { return Image.Count; } }
    int DrawHeight { get; set; }
    int DrawWidth { get; set; }
    MagickImage Frame { get { return GetFrame(); } }
    void UpdateValues();
    MagickImage GetFrame();
    List<string> GetLines();
    string GetLine(int num);
    string GetImageText();
    string GetPixelText(int x, int y, IPixelCollection<byte> pixels);
}