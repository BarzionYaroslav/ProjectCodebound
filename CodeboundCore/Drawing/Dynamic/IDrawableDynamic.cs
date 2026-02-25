using ImageMagick;
namespace Codebound.Drawing;
public interface IDrawableDynamic : IDrawable
{
    int X { get; set; }
    int Y { get; set; }
    int StartX { get; set; }
    int StartY { get; set; }
    int Depth { get; set; }
    void Draw(StageImage stage, int depth);
}