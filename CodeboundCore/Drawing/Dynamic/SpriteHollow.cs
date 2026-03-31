using ImageMagick;
using Codebound.System;
namespace Codebound.Drawing;

public class SpriteHollow
{
    public MagickImageCollection Image
    {
        get;
        private set;
    }
    public float ImageIndex
    {
        get;
        private set;
    }
    public float ImageSpeed
    {
        get;
        private set;
    }
    public float ImageCount
    {
        get;
        private set;
    }
    public int DrawHeight
    {
        get;
        private set;
    }
    public int DrawWidth
    {
        get;
        private set;
    }
    public int X
    {
        get;
        private set;
    }
    public int Y
    {
        get;
        private set;
    }
    public int Z
    {
        get;
        private set;
    }
    public int StartX
    {
        get;
        private set;
    }
    public int StartY
    {
        get;
        private set;
    }
    public int Depth
    {
        get;
        private set;
    }
    public bool Vanish
    {
        get;
        private set;
    }
    public MagickImage Frame
    {
        get;
        private set;
    }

    public SpriteHollow(Sprite spr)
    {
        Image = spr.Image;
        ImageIndex = spr.ImageIndex;
        ImageSpeed = spr.ImageSpeed;
        ImageCount = spr.ImageCount;
        DrawHeight = spr.DrawHeight;
        DrawWidth = spr.DrawWidth;
        X = spr.X;
        Y = spr.Y;
        Z = spr.Z;
        StartX = spr.StartX;
        StartY = spr.StartY;
        Depth = spr.Depth;
        Vanish = spr.Vanish;
        Frame = spr.Frame;
    }
}