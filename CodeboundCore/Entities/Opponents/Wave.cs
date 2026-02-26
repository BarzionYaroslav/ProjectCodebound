namespace Codebound.Entities.Opponents;
public class Wave
{
    private List<Enemy> _list = new List<Enemy>();
    public int Count {get { return _list.Count; }}
}