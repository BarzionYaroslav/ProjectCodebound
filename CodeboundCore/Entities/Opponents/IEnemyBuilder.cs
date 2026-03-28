namespace Codebound.Entities.Opponents;
public interface IEnemyBuilder<out T> where T: Enemy, new()
{
    T Build();
}