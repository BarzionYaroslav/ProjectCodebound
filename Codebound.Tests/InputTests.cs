using Codebound.System;
using Codebound.System.Control;
using Codebound.System.UI;
namespace Codebound.Tests;
public class InputTests
{
    [Fact]
    public void InitialKeyBind_KeyChangesWhenGetKeyIsCalled()
    {
        Panel panel = new Panel(180, 60, "testin");
        InputHandler inputHandler = new InputHandler();
        BattleManager.Instance.PrepareFight();

        inputHandler.BindKey(ConsoleKey.Z, new PanelSelectCommand(panel));
        ICommand answer = inputHandler.GetKey(ConsoleKey.Z);

        Assert.Equal("Codebound.System.Control.PanelSelectCommand", answer.GetType().ToString());
    }

    [Fact]
    public void InitialKeyBind_KeyCanBeInputted()
    {
        Panel panel = new Panel(180, 60, "testin");
        InputHandler inputHandler = new InputHandler();
        BattleManager.Instance.PrepareFight();

        inputHandler.BindKey(ConsoleKey.Z, new PanelSelectCommand(panel));
        inputHandler.HandleInput(ConsoleKey.Z);

        Assert.Equal("Codebound.System.UI.PanelStateEnemyFight", panel.GetState().ToString());
    }

    [Fact]
    public void KeyRebind_KeyChangesWhenGetKeyIsCalled()
    {
        Panel panel = new Panel(180, 60, "testin");
        InputHandler inputHandler = new InputHandler();
        BattleManager.Instance.PrepareFight();

        inputHandler.BindKey(ConsoleKey.Z, new PanelSelectCommand(panel));
        inputHandler.BindKey(ConsoleKey.J, new PanelSelectCommand(panel));
        ICommand answer = inputHandler.GetKey(ConsoleKey.J);

        Assert.Equal("Codebound.System.Control.PanelSelectCommand", answer.GetType().ToString());
    }

    [Fact]
    public void KeyRebind_KeyCanBeInputted()
    {
        Panel panel = new Panel(180, 60, "testin");
        InputHandler inputHandler = new InputHandler();
        BattleManager.Instance.PrepareFight();

        inputHandler.BindKey(ConsoleKey.Z, new PanelSelectCommand(panel));
        inputHandler.BindKey(ConsoleKey.J, new PanelSelectCommand(panel));
        inputHandler.HandleInput(ConsoleKey.J);

        Assert.Equal("Codebound.System.UI.PanelStateEnemyFight", panel.GetState().ToString());
    }
}