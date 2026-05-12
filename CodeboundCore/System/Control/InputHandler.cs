namespace Codebound.System.Control;

public class InputHandler
{
    private Dictionary<ConsoleKey, ICommand> _controls = new Dictionary<ConsoleKey, ICommand>();

    public void BindKey(ConsoleKey key, ICommand command)
    {
        if (_controls.ContainsKey(key))
        {
            _controls[key] = command;
        }
        else
        {
            _controls.Add(key, command);
        }
    }

    public void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            if (_controls.ContainsKey(key))
            {
                _controls[key].Execute();
            }
        }
    }

    public ICommand GetKey(ConsoleKey key)
    {
        if (_controls.ContainsKey(key))
            return _controls[key];
        else
            throw new KeyNotFoundException();
    }

    public void HandleInput(ConsoleKey key)
    {
        if (_controls.ContainsKey(key))
        {
            _controls[key].Execute();
        }
    }
}
