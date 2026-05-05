using Codebound.System;
using Codebound.System.UI;
namespace Codebound.Tests;
public class StateTests
{
    [Fact]
    public void MainState_OnZPress_GoToFightState()
    {
        Panel panel = new Panel(180, 60, "testin");
        BattleManager.Instance.PrepareFight();

        panel.HandleControls(ConsoleKey.Z);

        Assert.Equal("Codebound.System.UI.PanelStateEnemyFight", panel.GetState().ToString());
    }

    [Fact]
    public void MainState_OnZPress_GoToInventoryState()
    {
        Panel panel = new Panel(180, 60, "testin");
        BattleManager.Instance.PrepareFight();

        panel.HandleControls(ConsoleKey.DownArrow);
        panel.HandleControls(ConsoleKey.DownArrow);
        panel.HandleControls(ConsoleKey.Z);

        Assert.Equal("Codebound.System.UI.PanelStateInventory", panel.GetState().ToString());
    }

    [Fact]
    public void FightState_OnXPress_GoToMainState()
    {
        Panel panel = new Panel(180, 60, "testin");
        BattleManager.Instance.PrepareFight();

        panel.HandleControls(ConsoleKey.Z);
        panel.HandleControls(ConsoleKey.X);

        Assert.Equal("Codebound.System.UI.PanelStateBattleMain", panel.GetState().ToString());
    }

    [Fact]
    public void FightState_OnZPress_GoToMainState()
    {
        Panel panel = new Panel(180, 60, "testin");
        BattleManager.Instance.PrepareFight();

        panel.HandleControls(ConsoleKey.Z);
        panel.HandleControls(ConsoleKey.Z);

        Assert.Equal("Codebound.System.UI.PanelStateBattleMain", panel.GetState().ToString());
    }

    [Fact]
    public void InventoryState_OnZPress_GoToMainState()
    {
        Panel panel = new Panel(180, 60, "testin");
        BattleManager.Instance.PrepareFight();

        panel.HandleControls(ConsoleKey.DownArrow);
        panel.HandleControls(ConsoleKey.DownArrow);
        panel.HandleControls(ConsoleKey.Z);
        panel.HandleControls(ConsoleKey.Z);

        Assert.Equal("Codebound.System.UI.PanelStateBattleMain", panel.GetState().ToString());
    }

    [Fact]
    public void InventoryState_OnXPress_GoToMainState()
    {
        Panel panel = new Panel(180, 60, "testin");
        BattleManager.Instance.PrepareFight();

        panel.HandleControls(ConsoleKey.DownArrow);
        panel.HandleControls(ConsoleKey.DownArrow);
        panel.HandleControls(ConsoleKey.Z);
        panel.HandleControls(ConsoleKey.X);

        Assert.Equal("Codebound.System.UI.PanelStateBattleMain", panel.GetState().ToString());
    }
}