using System.ComponentModel;
using System.Runtime.ExceptionServices;

namespace Codebound.System.Randomness;
public class RandomList<T>: IRandomList<T>
{
    private List<T> _list = new List<T>();
    private IRandomProvider randomizer;
    public int Count {get { return _list.Count; }}
    public T this[int ind]
    {
        get
        {
            if (ind >= 0 && ind < Count)
                return _list[ind];
            else
                throw new ArgumentOutOfRangeException();
        }
    }
    public RandomList()
    {
        randomizer = GameManager.Instance.Randomizer;
    }
    public RandomList(IRandomProvider randomizer)
    {
        if (randomizer != null)
        {
            this.randomizer = randomizer;
        }
        else
        {
            this.randomizer = GameManager.Instance.Randomizer;
        }
    }
    public RandomList(List<T> elements)
    {
        this.randomizer = GameManager.Instance.Randomizer;
        foreach (T i in elements)
        {
            Add(i);
        }
    }
    public RandomList(List<T> elements, IRandomProvider randomizer)
    {
        if (randomizer != null)
        {
            this.randomizer = randomizer;
        }
        else
        {
            this.randomizer = GameManager.Instance.Randomizer;
        }
        foreach (T i in elements)
        {
            Add(i);
        }
    }
    public void Add(T element)
    {
        _list.Add(element);
    }

    public void RemoveAt(int ind)
    {
        _list.RemoveAt(ind);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _list.GetEnumerator();
    }
    public T GetRandom()
    {
        return this[randomizer.GetInt(Count)];
    }
}