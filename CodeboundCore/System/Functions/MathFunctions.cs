namespace Codebound.System.Functions;
public static class MathFunctions
{
    public static double DSin(float a)
    {
        return Math.Sin(a * (MathF.PI / 180));
    }

    public static double DCos(float a)
    {
        return Math.Cos(a * (MathF.PI / 180));
    }
    public static double DSin(int a) => DSin((float)a);

    public static double DCos(int a) => DCos((float)a);

    public static double DSin(double a) => DSin((float)a);

    public static double DCos(double a) => DCos((float)a);
}