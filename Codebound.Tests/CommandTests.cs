using Codebound.System;
using Codebound.System.Control;
using Codebound.System.UI;
namespace Codebound.Tests;
public class CommandTests
{
    [Fact]
    public void Panel_PanelSelectSelects()
    {
        Panel panel = new Panel(180, 60, "testin");
        BattleManager.Instance.PrepareFight();

        new PanelSelectCommand(panel).Execute();

        Assert.Equal("Codebound.System.UI.PanelStateEnemyFight", panel.GetState().ToString());
    }

    [Fact]
    public void Panel_PanelDownMovesChoice()
    {
        Panel panel = new Panel(180, 60, "testin");
        BattleManager.Instance.PrepareFight();

        new PanelDownCommand(panel).Execute();

        Assert.Equal(1, panel.Buttons.Choice);
    }

    [Fact]
    public void Panel_PanelUpMovesChoice()
    {
        Panel panel = new Panel(180, 60, "testin");
        BattleManager.Instance.PrepareFight();

        new PanelUpCommand(panel).Execute();

        Assert.Equal(panel.Buttons.Count-1, panel.Buttons.Choice);
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

    [Fact]
    public void Panel_PanelBackCommandGoesBack()
    {
        Panel panel = new Panel(180, 60, "testin");
        BattleManager.Instance.PrepareFight();

        new PanelSelectCommand(panel).Execute();
        new PanelBackCommand(panel).Execute();

        Assert.Equal("Codebound.System.UI.PanelStateBattleMain", panel.GetState().ToString());
    }
}