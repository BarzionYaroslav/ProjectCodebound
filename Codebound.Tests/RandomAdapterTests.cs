using Codebound.System.Randomness;
namespace Codebound.Tests;
public class RandomAdapterTests
{
    [Fact]
    public void RandomTest_Seed_ChangeWorksAsIntended()
    {
        RandomAdapter rand = new RandomAdapter();

        rand.SetSeed(100);

        Assert.Equal(100, rand.Seed);
    }

    [Fact]
    public void RandomTest_Randomness_Int_MaxGivesSameValueAsRandom()
    {
        RandomAdapter randAdap = new RandomAdapter(100);
        Random rand = new Random(100);

        int val1 = randAdap.GetInt(5);
        int val2 = rand.Next(5);

        Assert.Equal(val2, val1);
    }

    [Fact]
    public void RandomTest_Randomness_Int_MinMaxGivesSameValueAsRandom()
    {
        RandomAdapter randAdap = new RandomAdapter(100);
        Random rand = new Random(100);

        int val1 = randAdap.GetInt(5, 10);
        int val2 = rand.Next(5, 10);

        Assert.Equal(val2, val1);
    }

    [Fact]
    public void RandomTest_Randomness_Double_GivesSameValueAsRandom()
    {
        RandomAdapter randAdap = new RandomAdapter(100);
        Random rand = new Random(100);

        double val1 = randAdap.GetDouble();
        double val2 = rand.NextDouble();

        Assert.Equal(val2, val1);
    }
}