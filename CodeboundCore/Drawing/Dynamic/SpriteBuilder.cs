using ImageMagick;
using Codebound.System;
namespace Codebound.Drawing;
public class SpriteBuilder
{
    private Sprite _sprite = new Sprite();

    public SpriteBuilder SetSprite(string name)
    {
        var path = AssetManager.GetEntitySpritePath(name);
        if (File.Exists(path))
            _sprite.Image = new MagickImageCollection(path);
        _sprite.DrawHeight = (int)_sprite.Image[0].Height;
        _sprite.DrawWidth = (int)_sprite.Image[0].Width;
        return this;
    }

    public SpriteBuilder SetPosition(int x, int y)
    {
        _sprite.X = x;
        _sprite.Y = y;
        _sprite.StartX = _sprite.X;
        _sprite.StartY = _sprite.Y;
        return this;
    }

    public SpriteBuilder SetZ(int z)
    {
        _sprite.Z = z;
        return this;
    }

    public SpriteBuilder SetImageSpeed(float speed)
    {
        _sprite.ImageSpeed = speed;
        return this;
    }

    public SpriteBuilder SetDepth(int depth)
    {
        _sprite.Depth = depth;
        return this;
    }

    public Sprite Build() => _sprite;
}