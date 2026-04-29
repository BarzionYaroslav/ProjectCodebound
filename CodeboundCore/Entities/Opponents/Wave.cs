namespace Codebound.Entities.Opponents;
public class Wave
{
    private List<Enemy> _list = new List<Enemy>();
    public int Count { get { return _list.Count; } }
    
    public Enemy this[int ind]
    {
        get
        {
            if (ind >= 0 && ind < Count)
                return _list[ind];
            else
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Add(Enemy enm)
    {
        _list.Add(enm);
    }

    public void RemoveAt(int ind)
    {
        if (ind >= 0 && ind < Count)
        {
            _list[ind].Dispose();
            _list.RemoveAt(ind);
        }
        else
            throw new ArgumentOutOfRangeException();
    }

    public void Clear()
    {
        foreach (var i in _list)
        {
            i.Dispose();
        }
        _list.Clear();
    }

    public IEnumerator<Enemy> GetEnumerator()
    {
        return _list.GetEnumerator();
    }
}