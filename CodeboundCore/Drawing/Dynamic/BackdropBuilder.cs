using ImageMagick;
using Codebound.System;
namespace Codebound.Drawing;
public class BackdropBuilder
{
    private Backdrop _backdrop = new Backdrop();

    public BackdropBuilder SetSprite(string name)
    {
        var path = AssetManager.GetBackgroundSpritePath(name);
        if (File.Exists(path))
            _backdrop.Image = new MagickImageCollection(path);
        _backdrop.DrawHeight = (int)_backdrop.Image[0].Height;
        _backdrop.DrawWidth = (int)_backdrop.Image[0].Width;
        return this;
    }

    public BackdropBuilder SetPosition(int x, int y)
    {
        _backdrop.X = x;
        _backdrop.Y = y;
        _backdrop.StartX = _backdrop.X;
        _backdrop.StartY = _backdrop.Y;
        return this;
    }

    public BackdropBuilder SetImageSpeed(float speed)
    {
        _backdrop.ImageSpeed = speed;
        return this;
    }

    public BackdropBuilder SetDepth(int depth)
    {
        _backdrop.Depth = depth;
        return this;
    }

    public Backdrop Build() => _backdrop;
}