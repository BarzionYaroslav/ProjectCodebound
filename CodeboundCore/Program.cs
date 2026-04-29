using Codebound.System;
using Codebound.Drawing;
class Program
{
    static void Main()
    {
        Console.Clear();
        SoundManager.PlayBGM("shrine");
        Console.WriteLine("Choose a stage");
        Console.WriteLine("1: Bridge");
        Console.WriteLine("2: Weird Arena");
        Console.WriteLine("3: Training");
        string? choice = Console.ReadLine();
        SoundManager.PlayBGM("himorogi");
        switch (choice)
        {
            case "1":
                new BackdropBuilder().SetSprite("bridge/bridge_bg")
                            .SetDepth(16)
                            .SetImageSpeed(0.2f)
                            .Build()
                            .Init();
                new BackdropBuilder().SetSprite("bridge/bridge_clouds_back")
                            .SetDepth(15)
                            .SetScrollSpeed(-0.25f)
                            .Build()
                            .Init();
                new BackdropBuilder().SetSprite("bridge/bridge_clouds_front")
                            .SetDepth(14)
                            .SetScrollSpeed(-0.5f)
                            .Build()
                            .Init();
                new BackdropBuilder().SetSprite("bridge/bridge_water")
                            .SetDepth(13)
                            .SetImageSpeed(0.2f)
                            .Build()
                            .Init();
                new BackdropBuilder().SetSprite("bridge/bridge_main")
                            .SetDepth(12)
                            .Build()
                            .Init();
                break;
            case "2":
                new BackdropBuilder().SetSprite("WeirdArena")
                    .SetDepth(16)
                    .Build()
                    .Init();
                break;
            default:
                new BackdropBuilder().SetSprite("training_test")
                    .SetDepth(16)
                    .Build()
                    .Init();
                break;
        }
                    
        GameManager.Instance.GameLoop();
    }
}