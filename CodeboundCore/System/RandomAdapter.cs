namespace Codebound.System;
public class RandomAdapter: IRandomProvider
{
    private Random _adaptee;
    private int seed;
    public int Seed { get { return seed; } }
    
    public RandomAdapter()
    {
        int seeder = (int)DateTime.Now.Ticks;
        _adaptee = new Random(seeder);
        seed = seeder;
    }
    public RandomAdapter(int seed)
    {
        _adaptee = new Random(seed);
        this.seed = seed;
    }
    public void SetSeed(int seed)
    {
        this.seed = seed;
        _adaptee = new Random(seed);
    }
    public int GetInt(int max)
    {
        return _adaptee.Next(max);
    }
    public int GetInt(int min, int max)
    {
        return _adaptee.Next(min, max);
    }
    public double GetDouble()
    {
        return _adaptee.NextDouble();
    }
}