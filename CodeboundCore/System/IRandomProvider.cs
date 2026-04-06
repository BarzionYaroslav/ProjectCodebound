namespace Codebound.System;

public interface IRandomProvider
{
    public int Seed { get; }
    void SetSeed(int seed);
    int GetInt(int max);
    int GetInt(int min, int max);
    double GetDouble();
}