using Codebound.System;
using Codebound.Drawing;
using Codebound.Entities.Opponents;
class Program
{
    static void Main()
    {
        Backdrop bg = new BackdropBuilder().SetSprite("WeirdArena")
                    .SetPosition(0,0)
                    .SetDepth(16)
                    .SetImageSpeed(0)
                    .Build();
        GameManager.Instance.GameLoop();
    }
}