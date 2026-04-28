using Codebound.System;
using Codebound.Drawing;
class Program
{
    static void Main()
    {
        SoundManager.PlayBGM("himorogi");
        new BackdropBuilder().SetSprite("bridge/bridge_bg")
                    .SetDepth(16)
                    .SetImageSpeed(0.2f)
                    .Build();
        new BackdropBuilder().SetSprite("bridge/bridge_clouds_back")
                    .SetDepth(15)
                    .SetScrollSpeed(-0.25f)
                    .Build();
        new BackdropBuilder().SetSprite("bridge/bridge_clouds_front")
                    .SetDepth(14)
                    .SetScrollSpeed(-0.5f)
                    .Build();
        new BackdropBuilder().SetSprite("bridge/bridge_water")
                    .SetDepth(13)
                    .SetImageSpeed(0.2f)
                    .Build();
        Backdrop bg = new BackdropBuilder().SetSprite("bridge/bridge_main")
                    .SetDepth(12)
                    .Build();
                    
        GameManager.Instance.GameLoop();
    }
}