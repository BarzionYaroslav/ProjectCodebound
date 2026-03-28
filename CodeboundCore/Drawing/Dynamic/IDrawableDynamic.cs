using ImageMagick;
namespace Codebound.Drawing;
public interface IDrawableDynamic : IDrawable
{
    int X { get; }
    int Y { get; }
    int StartX { get; }
    int StartY { get; }
    int Depth { get; }
    void Draw(StageImage stage, int depth);
}