using Codebound.System;
using Codebound.Drawing;
using Codebound.Entities.Opponents;
class Program
{
    static void Main()
    {
        var bg = new Backdrop("WeirdArena", 0, 0, 0, 15);
        GameManager.Instance.GameLoop();
    }
}