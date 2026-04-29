namespace Codebound.Drawing;
public class ComplexSpriterHollow
{
    private Dictionary<string, SpriteHollow> _sprites = new Dictionary<string, SpriteHollow>();
    public SpriteHollow this[string name]
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
    }

    public ComplexSpriterHollow(ComplexSpriter spriter)
    {
        foreach (var i in spriter.Keys)
        {
            _sprites.Add(i, new SpriteHollow(spriter[i]));
        }
    }
}