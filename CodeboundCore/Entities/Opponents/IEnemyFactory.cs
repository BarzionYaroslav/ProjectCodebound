using Codebound.Drawing;

namespace Codebound.Entities.Opponents;

public interface IEnemyFactory
{
    int X { get; set; }
    int Y { get; set; }
    int Depth { get; set; }
    Enemy Create();
    Sprite MakeSprite(string asset, float imageSpeed);
}