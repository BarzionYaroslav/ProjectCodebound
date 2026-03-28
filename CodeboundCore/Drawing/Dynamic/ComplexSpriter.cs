namespace Codebound.Drawing;
public class ComplexSpriter: IDisposable
{
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    public Sprite this[string name]
    {
        get
        {
            if (name == null)
                throw new NullReferenceException();
            if (_sprites.ContainsKey(name))
                return _sprites[name];
            else
                throw new KeyNotFoundException();
        }
        set
        {
            if (name == null || value == null)
                throw new NullReferenceException();
            if (!expectedValues.Contains(name))
                throw new KeyNotFoundException();
            _sprites[name].Dispose();
            _sprites[name] = value;
        }
    }

    public ComplexSpriter(HashSet<string> expectations)
    {
        expectedValues = expectations;
        Fill();
    }

    public void ChangeExpectations(HashSet<string> expectations)
    {
        expectedValues = expectations;
        Fill();
    }

    public void Fill()
    {
        var keys = _sprites.Keys;
        var toBeAdded = new HashSet<string>(expectedValues);
        foreach (var key in keys)
        {
            if (!expectedValues.Contains(key))
            {
                _sprites[key].Dispose();
                _sprites.Remove(key);
            }
            else
                toBeAdded.Remove(key);
        }
        foreach (var add in toBeAdded)
            _sprites.Add(add, new Sprite());
    }

    public void Dispose()
    {
        var values = _sprites.Values;
        foreach (var val in values)
            val.Dispose();
    }

    public void Add(string name, Sprite spr)
    {
        if (expectedValues.Contains(name))
        {
            if (_sprites.ContainsKey(name))
            {
                _sprites[name] = spr;
            }
            else
            {
                _sprites.Add(name, spr);
            }
        }
    }
    public void Add(Dictionary<string, Sprite> dict)
    {
        foreach (var adder in dict.Keys)
        {
            Add(adder, dict[adder]);
        }
    }
    
    private HashSet<string> expectedValues = new HashSet<string>();
}