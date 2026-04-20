namespace Codebound.System.Functions;
public static class MathFunctions
{
    public static double DSin(int a)
    {
        return Math.Sin(a * (MathF.PI / 180));
    }

    public static double DCos(int a)
    {
        return Math.Cos(a * (MathF.PI / 180));
    }

    public static double DSin(float a)
    {
        return Math.Sin(a * (MathF.PI / 180));
    }

    public static double DCos(float a)
    {
        return Math.Cos(a * (MathF.PI / 180));
    }

    public static double DSin(double a)
    {
        return Math.Sin(a * (MathF.PI / 180));
    }

    public static double DCos(double a)
    {
        return Math.Cos(a * (MathF.PI / 180));
    }
}