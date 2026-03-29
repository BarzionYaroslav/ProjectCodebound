namespace Codebound.Drawing;
public class ComplexSpriterHollow
{
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    public SpriteHollow this[string name]
    {
        get
        {
            if (name == null)
                throw new NullReferenceException();
            if (_sprites.ContainsKey(name))
                return new SpriteHollow(_sprites[name]);
            else
                throw new KeyNotFoundException();
        }
    }

    public ComplexSpriterHollow(ComplexSpriter spriter)
    {
        foreach (var i in spriter.Keys)
        {
            _sprites.Add(i, spriter[i]);
        }
    }
}