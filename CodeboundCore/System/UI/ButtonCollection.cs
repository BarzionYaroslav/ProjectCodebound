using NetCoreAudio;
namespace Codebound.System.UI;

public class ButtonCollection
{
    private List<Button> _list = new List<Button>();
    public int Count { get { return _list.Count(); } }
    public int Choice
    {
        get { return choice; }
        set
        {
            if (value < 0)
                choice = Count - 1;
            else
                choice = value % Count;
        }
    }
    private int choice = 0;
    private Player sounder = new Player();

    public Button this[int ind]
    {
        get
        {
            if (ind >= 0 && ind < Count)
            {
                return _list[ind];
            }
            else
                throw new ArgumentOutOfRangeException($"Value was out of range: {ind} out of {Count}");
        }
    }

    public void Add(Button btn)
    {
        if (btn != null)
        {
            _list.Add(btn);
        }
        else
            throw new ArgumentNullException();
    }

    public void Remove(int ind)
    {
        if (ind >= 0 && ind < Count)
        {
            _list.RemoveAt(ind);
        }
        else
            throw new ArgumentOutOfRangeException();
    }

    public void ChangeChoice(int change, bool sound = false)
    {
        if (change != 0)
        {
            if (sound && (!sounder.Playing))
            {
                sounder.Play(ChangeSound);
            }
            Choice += change;
        }
    }

    public void AddChoice(bool sound = false)
    {
        this.ChangeChoice(1, sound);
    }

    public void SubstractChoice(bool sound = false)
    {
        this.ChangeChoice(-1, sound);
    }

    public void ExecuteChoice()
    {
        _list[choice].Action();
    }

    public string GetTextColor(int ind)
    {
        string starter = ColorReset;
        if (choice == ind)
            starter = DefaultSelectCol;
        return starter;
    }

    public IEnumerator<string> GetColorEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return GetTextColor(i);
        }
    }

    public IEnumerator<Button> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    const string DefaultSelectCol = "\e[38:5:220m";
    const string ColorReset = "\e[0m";
    const string ChangeSound = @"./assets/sounds/CursorMove.wav";
}