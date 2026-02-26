using Codebound.System;
using Codebound.Drawing;
using Codebound.Entities.Opponents;
class Program
{
    static void Main()
    {
        var punchySpr = new Sprite(@"./assets/punchy_bad.gif", 32, -6, 0.25f, 0);
        var arm1 = new Sprite(@"./assets/bad_arm1.gif", 0, -3, 0.25f, 0);
        var arm2 = new Sprite(@"./assets/bad_arm2.gif", 90-32, -3, 0.25f, 0);
        var punchyBad = new PunchyBad("Punchy Bad", 0, 0, 10, 10, punchySpr);
        var enmArm1 = new PunchyBadHand("Bad Hand", 0, 0, 10, 15, arm1, false);
        var enmArm2 = new PunchyBadHand("Bad Hand", 0, 0, 10, 15, arm2, true);
        var bg = new Backdrop(@"./assets/WeirdArena.gif", 0, 0, 0, 15);
        Game.GameLoop();
    }
}