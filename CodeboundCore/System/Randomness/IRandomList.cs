namespace Codebound.System.Randomness;
public interface IRandomList<T>
{
    void Add(T element);
    void RemoveAt(int ind);
    T GetRandom();
    List<T> ToList();
}