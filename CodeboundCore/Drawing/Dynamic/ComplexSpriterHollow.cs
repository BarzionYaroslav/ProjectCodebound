namespace Codebound.Drawing;
public class ComplexSpriterHollow
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
    }
}