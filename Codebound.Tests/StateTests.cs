using Codebound.System;
using Codebound.System.Control;
using Codebound.System.UI;
namespace Codebound.Tests;
public class StateTests
{
    [Fact]
    public void MainState_OnZPress_GoToFightState()
    {
        Panel panel = new Panel(180, 60, "testin");
        InputHandler inputHandler = new InputHandler();
        inputHandler.BindKey(ConsoleKey.DownArrow, new PanelDownCommand(panel));
        inputHandler.BindKey(ConsoleKey.UpArrow, new PanelUpCommand(panel));
        inputHandler.BindKey(ConsoleKey.Z, new PanelSelectCommand(panel));
        inputHandler.BindKey(ConsoleKey.X, new PanelBackCommand(panel));
        BattleManager.Instance.PrepareFight();

        inputHandler.HandleInput(ConsoleKey.Z);

        Assert.Equal("Codebound.System.UI.PanelStateEnemyFight", panel.GetState().ToString());
    }

    [Fact]
    public void MainState_OnZPress_GoToInventoryState()
    {
        Panel panel = new Panel(180, 60, "testin");
        InputHandler inputHandler = new InputHandler();
        inputHandler.BindKey(ConsoleKey.DownArrow, new PanelDownCommand(panel));
        inputHandler.BindKey(ConsoleKey.UpArrow, new PanelUpCommand(panel));
        inputHandler.BindKey(ConsoleKey.Z, new PanelSelectCommand(panel));
        inputHandler.BindKey(ConsoleKey.X, new PanelBackCommand(panel));
        BattleManager.Instance.PrepareFight();

        inputHandler.HandleInput(ConsoleKey.DownArrow);
        inputHandler.HandleInput(ConsoleKey.DownArrow);
        inputHandler.HandleInput(ConsoleKey.Z);

        Assert.Equal("Codebound.System.UI.PanelStateInventory", panel.GetState().ToString());
    }

    [Fact]
    public void FightState_OnXPress_GoToMainState()
    {
        Panel panel = new Panel(180, 60, "testin");
        InputHandler inputHandler = new InputHandler();
        inputHandler.BindKey(ConsoleKey.DownArrow, new PanelDownCommand(panel));
        inputHandler.BindKey(ConsoleKey.UpArrow, new PanelUpCommand(panel));
        inputHandler.BindKey(ConsoleKey.Z, new PanelSelectCommand(panel));
        inputHandler.BindKey(ConsoleKey.X, new PanelBackCommand(panel));
        BattleManager.Instance.PrepareFight();

        inputHandler.HandleInput(ConsoleKey.Z);
        inputHandler.HandleInput(ConsoleKey.X);

        Assert.Equal("Codebound.System.UI.PanelStateBattleMain", panel.GetState().ToString());
    }

    [Fact]
    public void FightState_OnZPress_GoToMainState()
    {
        Panel panel = new Panel(180, 60, "testin");
        InputHandler inputHandler = new InputHandler();
        inputHandler.BindKey(ConsoleKey.DownArrow, new PanelDownCommand(panel));
        inputHandler.BindKey(ConsoleKey.UpArrow, new PanelUpCommand(panel));
        inputHandler.BindKey(ConsoleKey.Z, new PanelSelectCommand(panel));
        inputHandler.BindKey(ConsoleKey.X, new PanelBackCommand(panel));
        BattleManager.Instance.PrepareFight();

        inputHandler.HandleInput(ConsoleKey.Z);
        inputHandler.HandleInput(ConsoleKey.Z);

        Assert.Equal("Codebound.System.UI.PanelStateBattleMain", panel.GetState().ToString());
    }

    [Fact]
    public void InventoryState_OnZPress_GoToMainState()
    {
        Panel panel = new Panel(180, 60, "testin");
        InputHandler inputHandler = new InputHandler();
        inputHandler.BindKey(ConsoleKey.DownArrow, new PanelDownCommand(panel));
        inputHandler.BindKey(ConsoleKey.UpArrow, new PanelUpCommand(panel));
        inputHandler.BindKey(ConsoleKey.Z, new PanelSelectCommand(panel));
        inputHandler.BindKey(ConsoleKey.X, new PanelBackCommand(panel));
        BattleManager.Instance.PrepareFight();

        inputHandler.HandleInput(ConsoleKey.DownArrow);
        inputHandler.HandleInput(ConsoleKey.DownArrow);
        inputHandler.HandleInput(ConsoleKey.Z);
        inputHandler.HandleInput(ConsoleKey.Z);

        Assert.Equal("Codebound.System.UI.PanelStateBattleMain", panel.GetState().ToString());
    }

    [Fact]
    public void InventoryState_OnXPress_GoToMainState()
    {
        Panel panel = new Panel(180, 60, "testin");
        InputHandler inputHandler = new InputHandler();
        inputHandler.BindKey(ConsoleKey.DownArrow, new PanelDownCommand(panel));
        inputHandler.BindKey(ConsoleKey.UpArrow, new PanelUpCommand(panel));
        inputHandler.BindKey(ConsoleKey.Z, new PanelSelectCommand(panel));
        inputHandler.BindKey(ConsoleKey.X, new PanelBackCommand(panel));
        BattleManager.Instance.PrepareFight();

        inputHandler.HandleInput(ConsoleKey.DownArrow);
        inputHandler.HandleInput(ConsoleKey.DownArrow);
        inputHandler.HandleInput(ConsoleKey.Z);
        inputHandler.HandleInput(ConsoleKey.X);

        Assert.Equal("Codebound.System.UI.PanelStateBattleMain", panel.GetState().ToString());
    }
}