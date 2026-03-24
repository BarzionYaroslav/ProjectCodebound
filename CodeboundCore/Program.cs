using Codebound.System;
using Codebound.Drawing;
class Program
{
    static void Main()
    {
        SoundManager.PlayBGM("himorogi");
        Backdrop bg = new BackdropBuilder().SetSprite("WeirdArena")
                    .SetPosition(0,0)
                    .SetDepth(16)
                    .SetImageSpeed(0)
                    .Build();
        GameManager.Instance.GameLoop();
    }
}